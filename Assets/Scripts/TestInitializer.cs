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
        XmlParse.ParseIdeology("ideology.xml");
        XmlParse.ParseCity("city.xml");
        Country.InitCountry("Turkey",City.cityList[0]);

        float presidentCorruption;
        Ideology presidentIdeology;
        // First we create current president.
        Person president =
            new Person
            (
                RandomName("names.txt"), RandomName("names.txt"),
                presidentIdeology = RandomIdeology(), 100,
                presidentCorruption = UnityEngine.Random.Range(10,maxCorruption)
            );
        Person vicePresident =
             new Person
             (
                 RandomName("names.txt"), RandomName("names.txt"),
                 presidentIdeology, 90,
                 UnityEngine.Random.Range(presidentCorruption-5, presidentCorruption)
             );
        // Then we create their first party.
        Party partyA = new Party(RandomName("partynames.txt"), "", president, presidentIdeology);
        government government = new government(partyA, president, vicePresident);

        government.cabinet.Add
                                (
                                new government.Ministry
                                        (
                                            "Ministry of Culture",
                                            new Person(
                                                        RandomName("names.txt"),
                                                        RandomName("names.txt"),
                                                        presidentIdeology,
                                                        president.fame - UnityEngine.Random.Range(10, 30),
                                                        president.corruption - UnityEngine.Random.Range(10, 30),
                                                        true),
                                            Country.ChangeableStats.baseCulture
                                        )
                                );
        government.cabinet.Add
                                (
                                new government.Ministry
                                        (
                                            "Ministry of Education",
                                            new Person(
                                                        RandomName("names.txt"),
                                                        RandomName("names.txt"),
                                                        presidentIdeology,
                                                        president.fame - UnityEngine.Random.Range(10, 30),
                                                        president.corruption - UnityEngine.Random.Range(10, 30),
                                                        true),
                                            Country.ChangeableStats.baseEducation
                                        )
                                );
        government.cabinet.Add
                                (
                                new government.Ministry
                                        (
                                            "Ministry of Military",
                                            new Person(
                                                        RandomName("names.txt"),
                                                        RandomName("names.txt"),
                                                        presidentIdeology,
                                                        president.fame - UnityEngine.Random.Range(10, 30),
                                                        president.corruption - UnityEngine.Random.Range(10, 30),
                                                        true),
                                            Country.ChangeableStats.baseMilitary
                                        )
                                );
        government.cabinet.Add
                                (
                                new government.Ministry
                                        (
                                            "Ministry of External Affairs",
                                            new Person(
                                                        RandomName("names.txt"),
                                                        RandomName("names.txt"),
                                                        presidentIdeology,
                                                        president.fame - UnityEngine.Random.Range(10, 30),
                                                        president.corruption - UnityEngine.Random.Range(10, 30),
                                                        true),
                                            Country.ChangeableStats.baseDiplomacy
                                        )
                                );



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
