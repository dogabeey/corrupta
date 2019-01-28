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
        public string ideology;
        public float rate;
        public IdeologyRate()
        {

        }
        public IdeologyRate(string ideology, float rate)
        {
            this.ideology = ideology;
            this.rate = rate;
        }
        
        public Ideology GetIdeology()
        {
            return Ideology.ideologyList.Find(i => i.ideologyName == ideology);
        }
    }
    public string cityName;
    public string description;
    public List<IdeologyRate> ideologyRates;

    public City()
    {

    }
    public City(string name, string description,List<IdeologyRate> ideologyRates)
    {
        this.cityName = name;
        this.description = description;
        this.ideologyRates = ideologyRates;
        cityList.Add(this);
    }
}
