using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PoolDictionary
{
    public Pool poolType;
    public List<GameObject> pooledObjects;

    public bool ContainsKey(Pool key)
    {
        return poolType == key;
    }
}

public class PoolingSystem : SerializedMonoBehaviour
{
    //public Dictionary<Pool, GameObject> poolPrefabs = new Dictionary<Pool, GameObject>();
    public List<PoolDictionary> poolDictionary;

    public Dictionary<Pool, Queue<GameObject>> poolDictonaryQueue;

    public void Awake()
    {
        poolDictonaryQueue = new Dictionary<Pool, Queue<GameObject>>();
        // Convert List to Dictionary of Queues
        foreach (var poolDict in poolDictionary)
        {
            poolDictonaryQueue[poolDict.poolType] = new Queue<GameObject>(poolDict.pooledObjects);
        }
    }
    public T Spawn<T>(Pool poolType, Vector3 position, Quaternion rotation) where T : Component
    {
        Queue<GameObject> poolQueue = poolDictonaryQueue.FirstOrDefault(pd => pd.Key == poolType).Value as Queue<GameObject>;
        if (poolQueue == null)
        {
            Debug.LogError("Pool type not found: " + poolType);
            return null;
        }
        GameObject obj;
        if (poolQueue.Count > 0)
        {
            obj = poolQueue.Dequeue() as GameObject;
            obj.SetActive(true);
        }
        else
        {
            Debug.LogError("No objects available in pool: " + poolType);
            return null;
        }
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj.GetComponent<T>();
    }
    public T Despawn<T>(Pool poolType, T obj) where T : Component
    {
            Queue<GameObject> poolQueue = poolDictionary.FirstOrDefault(pd => pd.poolType == poolType)?.pooledObjects.ToQueue();
        if (poolQueue == null)
        {
            Debug.LogError("Pool type not found: " + poolType);
            return null;
        }
        obj.gameObject.SetActive(false);
        poolQueue.Enqueue(obj.gameObject);
        return obj;
    }
}

public enum Pool
{
    CityText
}

public static class PoolExtensions
{
    public static T Spawn<T>(this Pool pool, Vector3 position, Quaternion rotation) where T : MonoBehaviour
    {
        return GameManager.Instance.poolingSystem.Spawn<T>(pool, position, rotation);
    }
    public static void Despawn<T>(this Pool pool, T obj) where T : MonoBehaviour
    {
        GameManager.Instance.poolingSystem.Despawn<T>(pool, obj);
    }
}