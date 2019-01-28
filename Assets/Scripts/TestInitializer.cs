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
        XmlParse.ParseIdeology("ideology.xml");

        new City("stormwind", "capital of human kingdom", new List<City.IdeologyRate>() { new City.IdeologyRate("religionism", 55), new City.IdeologyRate("liberalism", 45) });
        new Person("Recep Tayyip", "Erdoğan", "religionism", 100, 100, true);
        Party tempParty = new Party("Template Party","", "cyan", new Person("Template", "de Person", "religionism"), new Person("Temp", "von Chairman", "religionism"), new Person("Vice", "McPrincipal", "religionism"), "religionism");


        XmlParse.ExportAll(Person.people);
        XmlParse.ExportAll(City.cityList);
        XmlParse.ExportAll(Party.parties);

        Country.RandomizeAll();
        GameObject.FindGameObjectWithTag("uicontroller").SetActive(true);
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
