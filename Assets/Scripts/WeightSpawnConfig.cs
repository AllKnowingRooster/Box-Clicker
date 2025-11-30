using UnityEngine;

[CreateAssetMenu(fileName = "WeightSpawnConfig", menuName = "ScriptableObjects/WeightSpawnConfig")]
public class WeightSpawnConfig : ScriptableObject
{
    public SpawnableObjectConfig objConfig;
    [Range(0, 1)]
    [SerializeField] private float minWeight;
    [Range(0, 1)]
    [SerializeField] private float maxWeight;

    public float GetWeight()
    {
        return Random.Range(minWeight, maxWeight);
    }
}
