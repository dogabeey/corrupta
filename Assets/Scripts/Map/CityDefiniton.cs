using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New City Definition", menuName = "Corrupta/New City Definition...")]
public class CityDefiniton : ListedScriptableObject<CityDefiniton>
{
    public City city;
    [ColorUsage(false, false)]
    public Color mapColor;

    public Color Color => new Color(mapColor.r / 255f, mapColor.g / 255f, mapColor.b / 255f);
}
