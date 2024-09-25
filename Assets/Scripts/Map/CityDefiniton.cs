using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New City Definition", menuName = "Corrupta/New City Definition...")]
public class CityDefiniton : ListedScriptableObject<CityDefiniton>
{
    public City city;
    public int r, g, b;

    public Color Color => new Color(r / 255f, g / 255f, b / 255f);
}
