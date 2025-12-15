using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lionsfall.SimpleJSON;
using Lionsfall;
using System.Globalization;
using System;

[CreateAssetMenu(fileName = "New City Definition", menuName = "Corrupta/New City Definition...")]
public class CityDefiniton : ListedScriptableObject<CityDefiniton>, ISaveable
{
    public City city;
    [ColorUsage(false, false)]
    public Color mapColor;

    public Color Color => new Color(mapColor.r, mapColor.g, mapColor.b);

    public string SaveId => "city_definition_" + city.name.ToLower();

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


        saveData["city_id"] = city.id;
        saveData["mapColor_r"] =(int)mapColor.r;
        saveData["mapColor_g"] = (int)mapColor.g;
        saveData["mapColor_b"] =(int)mapColor.b;

        return saveData;
    }

    public bool Load(Action onLoadSuccess = null, Action onLoadFail = null)
    {

        JSONNode loadData = SaveManager.Instance.LoadSave(this);
        
        if (loadData != null)
        {
            int cityId = loadData["city_id"].AsInt;
            city = GameManager.Instance.cities.Find(x => x.id == cityId);
            mapColor.r = loadData["mapColor_r"].AsFloat;
            mapColor.g = loadData["mapColor_g"].AsFloat;
            mapColor.b = loadData["mapColor_b"].AsFloat;

            return true;
        }
        else
        {
            return false;
        }
    }
}
