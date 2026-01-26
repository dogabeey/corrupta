using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "New Ideology", menuName = "Corrupta/New Ideology...")]
public class Ideology  : ListedScriptableObject<Ideology>
{
    public string ideologyName;
    [TextArea(3,10)]
    public string description;

    private void OnValidate()
    {
        // Make asset's file name match ideology's name
        if (this.name != id + " - " + ideologyName)
        {
            this.name = id + " - " + ideologyName;
            UnityEditor.AssetDatabase.RenameAsset(UnityEditor.AssetDatabase.GetAssetPath(this), this.name);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }
    }

}
 