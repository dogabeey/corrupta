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
    [ReadOnly]
    public string ideologyName;
    public string description;

    public override void Start()
    {
    }
    public override void Update()
    {
    }
    public override void OnManagerDestroy()
    {
    }

    private void OnValidate()
    {
        string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
        ideologyName = Path.GetFileNameWithoutExtension(assetPath);
    }
}
 