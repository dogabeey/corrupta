using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Eppy;

public class City
{
    public static List<City> cityList = new List<City>();

    public int id;
    public string cityName;
    public string description;
    public List<CitizenGroup> citizensList;

    public int mayorId;

    public Person Mayor
    {
        get
        {
            return Person.people.Find(p => p.uuid == mayorId);
        }
    }

    public City()
    {

    }
    public City(int id,string name, string description)
    {
        this.id = id;
        this.cityName = name;
        this.description = description;
        cityList.Add(this);
    }

}
