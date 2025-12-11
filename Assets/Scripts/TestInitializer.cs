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

        XmlParse.ImportAll<CityDefiniton>();
        XmlParse.ImportAll<City>();
        XmlParse.ImportAll<Ideology>();
        XmlParse.ImportAll<Party>();
        XmlParse.ImportAll<Country>();

        Country.RandomizeAll();
        
	}
}
