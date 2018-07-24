using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Eppy;

public class City
{
    public string cityName;
    public string description;
    public static List<City> cityList = new List<City>();

    public Tuple<Ideology, float>[] ideologyRates;

    public City(string name, string description)
    {
        this.cityName = name;
        this.description = description;
        ideologyRates = new Tuple<Ideology, float>[30];

        cityList.Add(this);
    }
}
