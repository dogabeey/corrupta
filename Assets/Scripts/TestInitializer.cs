﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestInitializer : MonoBehaviour
{
    public int minAge, maxAge;
    public float maxFame;
    public float maxCorruption;

    void Start ()
    {
        Country.InitCountry("Turkey", "stormwind");

        XmlParse.ImportAll<CityDefiniton>();
        XmlParse.ImportAll<City>();
        XmlParse.ImportAll<Ideology>();
        XmlParse.ImportAll<Party>();
        XmlParse.ImportAll<Country>();

        Country.RandomizeAll();
        
	}

    public Ideology RandomIdeology()
    {
        int index = UnityEngine.Random.Range(0,Ideology.GetInstances().Count);
        return Ideology.GetInstances()[index];
    }

    public City RandomCity()
    {
        int index = UnityEngine.Random.Range(0,City.GetInstances().Count);
        return City.GetInstances()[index];
    }

    public Party RandomParty()
    {
        int index = UnityEngine.Random.Range(0,City.GetInstances().Count);
        return Party.parties[index];
    }

    public string RandomName(string fileName)
    {
        string[] nameList = File.ReadAllLines("Assets\\XML\\" + fileName);
        string retVal = nameList[UnityEngine.Random.Range(0,nameList.Length)];
        return retVal;
    }
}
