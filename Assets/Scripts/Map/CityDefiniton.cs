using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lionsfall.SimpleJSON;
using Lionsfall;

[CreateAssetMenu(fileName = "New City Definition", menuName = "Corrupta/New City Definition...")]
public class CityDefiniton : ListedScriptableObject<CityDefiniton>, ISaveable
{
    public City city;
    [ColorUsage(false, false)]
    public Color mapColor;

    public Color Color => new Color(mapColor.r, mapColor.g, mapColor.b);

    public string SaveId => "city_definition_" + id;

    public override void Start()
    {
        SaveManager.Instance.Register(this);
    }
    public override void Update()
    {
        
    }
    public override void OnManagerDestroy()
    {
        
    }

    public SaveDataType SaveDataType => SaveDataType.WorldProgression;

    public Dictionary<string, object> Save()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        
        saveData["city"] = city;
        saveData["mapColor_r"] =(int)mapColor.r;
        saveData["mapColor_g"] = (int)mapColor.g;
        saveData["mapColor_b"] =(int)mapColor.b;

        return saveData;
    }

    public void Load()
    {

        JSONNode loadData = SaveManager.Instance.LoadSave(this);
        
        if (loadData != null)
        {
            city.name = loadData["city"].AsObject;
            mapColor.r = loadData["mapColor_r"].AsFloat;
            mapColor.g = loadData["mapColor_g"].AsFloat;
            mapColor.b = loadData["mapColor_b"].AsFloat;
        }
    }
}
