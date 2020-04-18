using System.Collections;
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
        //XmlParse.ParseIdeology("ideology.xml");
        Person.people = XmlParse.ImportAll<Person>();
        City.cityList = XmlParse.ImportAll<City>();

        new CityDefiniton(1, 102, 194, 165);
        new CityDefiniton(6, 161, 220, 125);
        new CityDefiniton(34, 252, 141, 98);
        new CityDefiniton(35, 231, 138, 195);
        new CityDefiniton(69, 164, 196, 200);
        new CityDefiniton(77, 192, 128, 100);
        new CityDefiniton(82, 255, 217, 47);
        new CityDefiniton(83, 205, 218, 91);
        new CityDefiniton(84, 213, 162, 221);
        new CityDefiniton(666, 108, 227, 170);

        Party.parties = XmlParse.ImportAll<Party>();

        XmlParse.ExportAll(CityDefiniton.cityDefs);

        Country.RandomizeAll();
        
	}

    public Ideology RandomIdeology()
    {
        int index = UnityEngine.Random.Range(0,Ideology.ideologyList.Count);
        return Ideology.ideologyList[index];
    }

    public City RandomCity()
    {
        int index = UnityEngine.Random.Range(0,City.cityList.Count);
        return City.cityList[index];
    }

    public string RandomName(string fileName)
    {
        string[] nameList = File.ReadAllLines("Assets\\XML\\" + fileName);
        string retVal = nameList[UnityEngine.Random.Range(0,nameList.Length)];
        return retVal;
    }
}
