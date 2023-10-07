using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Occupation
{
    public int id;
    public string className;
    public float avgWealth;
    public float avgEducation;
    public float avgPartizanship;

    public Occupation(string className, float avgWealth, float avgEducation, float avgPartizanship)
    {
        this.className = className;
        this.avgWealth = avgWealth;
        this.avgEducation = avgEducation;
        this.avgPartizanship = avgPartizanship;

        occupations.Add(this);
    }

    public static List<Occupation> occupations = new List<Occupation>();

    public Occupation()
    {

    }
}