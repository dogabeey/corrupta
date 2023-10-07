using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Eppy;

[CreateAssetMenu(fileName = "New City", menuName = "Corrupta/New City...")]
public class City : ListedScriptableObject<City>
{
    public static List<City> cityList = new List<City>();

    public int id;
    public string cityName;
    public string description;
    public Person mayor;

}
