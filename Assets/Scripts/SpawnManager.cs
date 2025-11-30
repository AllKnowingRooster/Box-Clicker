using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }
    private Dictionary<int, Pool> dictPool;
    [SerializeField] private List<WeightSpawnConfig> listPrefabsMainGame;
    [SerializeField] private SpawnMethod spawnMethod;
    [SerializeField] private BoxCollider spawnArea;
    private float[] weights;
    private float minZRotation;
    private float maxZRotation;
    private float minBound;
    private float maxBound;
    private float topBound;
    [SerializeField] private Transform particleParent;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        minBound = spawnArea.bounds.min.x;
        maxBound = spawnArea.bounds.max.x;
        topBound = spawnArea.bounds.max.y;
        minZRotation = 0.0f;
        maxZRotation = 360.0f;
        weights = new float[listPrefabsMainGame.Count];
        dictPool = new Dictionary<int, Pool>();
        for (int i = 0; i < listPrefabsMainGame.Count; i++)
        {
            dictPool[i] = Pool.CreatePool(listPrefabsMainGame[i].objConfig.obj, 20);
        }
    }


    public IEnumerator SpawnSpawnableObject()
    {
        while (GameManager.instance.lives > 0)
        {
            ResetWeight();
            SpawnSingleSpawnableObject();
            yield return new WaitForSeconds(Mathf.Clamp((3.0f - (GameManager.instance.timer / 30.0f)), 0.25f, 1.5f));
        }
    }

    private void SpawnSingleSpawnableObject()
    {
        int index = -1;
        if (spawnMethod == SpawnMethod.Random)
        {
            index = RandomSpawn();
        }
        else
        {
            index = WeightedSpawn();
        }

        PoolableObject poolObj = dictPool[index].GetFromPool();
        if (poolObj != null)
        {
            SpawnableObject spawnableObj = poolObj.GetComponent<SpawnableObject>();
            listPrefabsMainGame[index].objConfig.Setup(spawnableObj);
            spawnableObj.gameObject.SetActive(true);
            spawnableObj.gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(minZRotation, maxZRotation));
            spawnableObj.transform.position = new Vector3(Random.Range(minBound, maxBound), topBound, spawnArea.gameObject.transform.position.z);
        }
    }

    private void ResetWeight()
    {
        float totalWeight = 0.0f;
        for (int i = 0; i < listPrefabsMainGame.Count; i++)
        {
            weights[i] = listPrefabsMainGame[i].GetWeight();
            totalWeight += weights[i];
        }

        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] /= totalWeight;
        }
    }

    private int WeightedSpawn()
    {
        float val = Random.value;
        int index = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i] >= val)
            {
                index = i;
                break;
            }
            val -= weights[i];
        }
        return index;
    }

    private int RandomSpawn()
    {
        return Random.Range(0, listPrefabsMainGame.Count);
    }

    public void PlayVFX(ParticleSystem destroyParticle)
    {
        ParticleSystem particleSystem = Instantiate(destroyParticle, BoxChecker.point, Quaternion.identity, particleParent);

        float duration = particleSystem.main.duration;
        Destroy(particleSystem.gameObject, duration);
    }

}
