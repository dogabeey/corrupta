using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Eppy;

public class City
{
    public static List<City> cityList = new List<City>();
    public class IdeologyRate
    {
        public string ideologyString;
        public float rate;

        Ideology ideology;
        public IdeologyRate()
        {

        }
        public IdeologyRate(string ideologyString, float rate)
        {
            ideology = Ideology.ideologyList.Find(i => i.ideologyName == ideologyString);
            this.rate = rate;
        }

        public Ideology GetIdeology()
        {
            return Ideology.ideologyList.Find(i => i.ideologyName == ideologyString);
        }
    }

    public string cityName;
    public string description;
    public Vector2 coordinates;
    public List<IdeologyRate> ideologyRates;

    Person mayor;
    public int mayorId;

    public City()
    {

    }
    public City(string name, string description,Vector2 coordinates,List<IdeologyRate> ideologyRates)
    {
        this.cityName = name;
        this.description = description;
        this.coordinates = coordinates;
        this.ideologyRates = ideologyRates;
        cityList.Add(this);
    }

    public Person GetMayor()
    {
        return Person.people.Find(p => p.uuid == mayorId);
    }
}
