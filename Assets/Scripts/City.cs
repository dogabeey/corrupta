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
        public int cityId;
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
            this.ideologyString = ideologyString;
        }

        public Ideology GetIdeology()
        {
            return Ideology.ideologyList.Find(i => i.ideologyName == ideologyString);
        }
    }

    public int id;
    public string cityName;
    public string description;
    public List<IdeologyRate> ideologyRates;

    Person mayor;
    public int mayorId;

    public City()
    {

    }
    public City(int id,string name, string description,List<IdeologyRate> ideologyRates)
    {
        this.id = id;
        this.cityName = name;
        this.description = description;
        this.ideologyRates = ideologyRates;
        cityList.Add(this);
    }

    public Person GetMayor()
    {
        return Person.people.Find(p => p.uuid == mayorId);
    }
}
