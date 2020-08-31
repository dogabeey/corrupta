using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[XmlInclude(typeof(Occupation))]
public abstract class SocialClass
{
    public string className;
    public float avgWealth;
    public float avgEducation;
    public float avgPartizanship;
    public float avgCorruptability;
    public float avgRebelliousness;
    public float ratio = 1;
    public static List<SocialClass> socialClasses = new List<SocialClass>();


    public SocialClass()
    {

    }
    protected SocialClass(string className, float avgWealth, float avgEducation, float avgPartizanship, float avgCorruptability, float avgRebelliousness, float ratio = 1)
    {
        this.className = className;
        this.avgWealth = avgWealth;
        this.avgEducation = avgEducation;
        this.avgPartizanship = avgPartizanship;
        this.avgCorruptability = avgCorruptability;
        this.avgRebelliousness = avgRebelliousness;
        this.ratio = ratio;

        socialClasses.Add(this);
    }
}

public class Occupation : SocialClass
{
    public Occupation()
    {

    }
    public Occupation(string className, float avgWealth, float avgEducation, float avgPartizanship, float avgCorruptability, float avgRebelliousness, float ratio) : base(className, avgWealth, avgEducation, avgPartizanship, avgCorruptability, avgRebelliousness, ratio)
    {
    }
}