using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public abstract class  ManageableScriptableObject : SerializedScriptableObject
{
}

public class ListedScriptableObject<T> : ManageableScriptableObject where T : ListedScriptableObject<T>
{
    [Button("Add New Instance", Icon = SdfIconType.Newspaper)]
    // Add a new instance of this type's scriptable object to the same folder as this scriptable object
    public void AddNewInstance(string objectName)
    {   T instance = CreateInstance<T>();
        string path = UnityEditor.AssetDatabase.GetAssetPath(this);
        string directory = System.IO.Path.GetDirectoryName(path);
        string assetPathAndName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(directory + "/" + objectName + ".asset");
        UnityEditor.AssetDatabase.CreateAsset(instance, assetPathAndName);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.EditorUtility.FocusProjectWindow();
        UnityEditor.Selection.activeObject = instance;
    }

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