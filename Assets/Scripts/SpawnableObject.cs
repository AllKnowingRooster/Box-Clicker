
using UnityEngine;

public class SpawnableObject : PoolableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 rotation;
    [HideInInspector] public int score;
    [HideInInspector] public ParticleSystem destroyParticle;
    private void Awake()
    {
        rotation = new Vector3(0, 5.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }

    public virtual void OnClick()
    {
        gameObject.SetActive(false);
        SpawnManager.instance.PlayVFX(destroyParticle);
    }
}
