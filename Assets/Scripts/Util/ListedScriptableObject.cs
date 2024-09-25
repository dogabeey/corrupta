using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
public class ListedScriptableObject<T> : SerializedScriptableObject
{
    public T GetRandomInstance()
    {
        int randomIndex = Random.Range(0, GetInstances().Count - 1);
        return GetInstances()[randomIndex];
    }

    public static List<T> GetInstances()
    {
        UnityEngine.Object[] loadedObjects = Resources.LoadAll("", typeof(T));
        List<T> instances = new List<T>();

        foreach (var obj in loadedObjects)
        {
            if (obj is T)
            {
                instances.Add((T)(object) obj);
            }
        }

        return instances;
    }
}