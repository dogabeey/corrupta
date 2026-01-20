using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class  ManageableScriptableObject : SerializedScriptableObject
{
    public abstract void Start();
    public abstract void Update();
    public abstract void OnManagerDestroy();
}

public abstract class ListedScriptableObject<T> : ManageableScriptableObject where T : ListedScriptableObject<T>
{
    public int id;


#if UNITY_EDITOR
    [Button("Add New Instance", Icon = SdfIconType.Newspaper)]
    // Add a new instance of this type's scriptable object to the same folder as this scriptable object
    public void AddNewInstance(string objectName)
    {
        T instance = CreateInstance<T>();
        string path = UnityEditor.AssetDatabase.GetAssetPath(this);
        string directory = System.IO.Path.GetDirectoryName(path);
        string assetPathAndName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(directory + "/" + objectName + ".asset");
        UnityEditor.AssetDatabase.CreateAsset(instance, assetPathAndName);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.EditorUtility.FocusProjectWindow();
        UnityEditor.Selection.activeObject = instance;
    }
    // Add a new instance of this type's scriptable object to the same folder as this scriptable object
    public T1 AddNewInstanceAlloc<T1>(string objectName) where T1 : ListedScriptableObject<T1>
    {
        T1 instance = CreateInstance<T1>();
        string path = UnityEditor.AssetDatabase.GetAssetPath(this);
        string directory = System.IO.Path.GetDirectoryName(path);
        string assetPathAndName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(directory + "/" + objectName + ".asset");
        UnityEditor.AssetDatabase.CreateAsset(instance, assetPathAndName);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.EditorUtility.FocusProjectWindow();
        UnityEditor.Selection.activeObject = instance;

        return instance;
    }
#endif

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
    public static T GetInstanceByID(int id)
    {
        List<T> instances = GetInstances();
        foreach (var instance in instances)
        {
            if (instance.id == id)
            {
                return instance;
            }
        }
        return null;
    }
    public static List<T> GetRuntimeInstancesFromAssets()
    {
        T[] loadedObjects = Resources.LoadAll<T>(""); // or your folder path
        List<T> instances = new List<T>(loadedObjects.Length);

        foreach (var obj in loadedObjects)
        {
            // Create a runtime clone so the original asset stays untouched
            T runtimeInstance = Instantiate(obj);
            instances.Add(runtimeInstance);
        }

        return instances;
    }
}