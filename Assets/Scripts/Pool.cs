using System.Collections.Generic;
using UnityEngine;
public class Pool
{
    private PoolableObject prefab;
    private int count;
    private List<PoolableObject> listPooledObject;

    private Pool(PoolableObject prefab, int count)
    {
        this.prefab = prefab;
        this.count = count;
        listPooledObject = new List<PoolableObject>();
    }


    public static Pool CreatePool(PoolableObject prefab, int count)
    {
        Pool pool = new Pool(prefab, count);
        Transform parent = new GameObject(prefab.name + " Pool").transform;
        for (int i = 0; i < count; i++)
        {
            PoolableObject poolObj = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            poolObj.pool = pool;
            poolObj.gameObject.SetActive(false);
        }
        return pool;
    }

    public PoolableObject GetFromPool()
    {
        if (listPooledObject.Count == 0)
        {
            return null;
        }
        PoolableObject poolableObject = listPooledObject[0];
        listPooledObject.RemoveAt(0);
        return poolableObject;
    }

    public void ReturnToPool(PoolableObject obj)
    {
        listPooledObject.Add(obj);
    }



}
