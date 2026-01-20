using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using Sirenix.OdinInspector;
using System.Linq;

[CreateAssetMenu(fileName = "New City Definition", menuName = "Corrupta/New City Definition...")]
public class CityDefiniton : ListedScriptableObject<CityDefiniton>
{
    public City city;
    [ColorUsage(false, false)]
    public Color mapColor;

    public Color Color => new Color(mapColor.r, mapColor.g, mapColor.b);

    public string SaveId => "city_definition_" + city.name.ToLower();


    public void OnValidate()
    {
        //Look for a city with the same name and assign it if exists
        if (city == null)
        {
            City foundCity = Resources.LoadAll<City>("").First(x => x.name == this.name.Replace("Def", ""));
            if (foundCity != null)
            {
                city = foundCity;
            }
        }

    }
    public override void Start()
    {
    }
    public override void Update()
    {
        
    }
    public override void OnManagerDestroy()
    {
        
    }

#if UNITY_EDITOR
    [Button]
    public void GenerateCityDefinitionsForEachColor(Texture2D provinceMap)
    {
        List<Color> colors = new List<Color>();
        colors = provinceMap.GetUniqueColors();
        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i] == Color.black) continue; // Skip black color


            CityDefiniton newCityDef = AddNewInstanceAlloc<CityDefiniton>("CityDef_" + i);
            newCityDef.name = "CityDef_" + i;
            newCityDef.id = i;
            newCityDef.mapColor = colors[i];

            City newCity = AddNewInstanceAlloc<City>("City_" + i);
            newCity.name = "City_" + i;
            newCity.id = i;

            newCityDef.city = newCity;
        }
    }
#endif
}
