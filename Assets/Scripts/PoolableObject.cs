using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public Pool pool;
    public virtual void OnDisable()
    {
        pool.ReturnToPool(this);
    }
}
