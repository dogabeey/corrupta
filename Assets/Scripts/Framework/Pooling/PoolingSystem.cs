using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem<T> : MonoBehaviour where T : MonoBehaviour
{
    public Dictionary<Pool, Queue<GameObject>> poolDictionary = new Dictionary<Pool, Queue<GameObject>>();

    private void CreatePool(Pool poolType, GameObject prefab, int initialSize)
    {
        if (!poolDictionary.ContainsKey(poolType))
        {
            poolDictionary[poolType] = new Queue<GameObject>();
            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                poolDictionary[poolType].Enqueue(obj);
            }
        }
    }
    public T Spawn(Pool poolType, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(poolType) && poolDictionary[poolType].Count > 0)
        {
            GameObject obj = poolDictionary[poolType].Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj.GetComponent<T>();
        }
        else
        {
            Debug.LogWarning("No objects available in pool: " + poolType);
            return null;
        }
    }
}

public enum Pool
{
    CityText
}

public static class PoolExtensions
{
    public static void Spawn(this Pool pool)
    {
    }
}