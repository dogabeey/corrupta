using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SocialClass
{
    public float avgWealth;
    public float avgEducation;
    public float avgPartizanship;
    public float avgCorruptability;
    public float avgRebelliousness;

    protected SocialClass(float avgWealth, float avgEducation, float avgPartizanship, float avgCorruptability, float avgRebelliousness)
    {
        this.avgWealth = avgWealth;
        this.avgEducation = avgEducation;
        this.avgPartizanship = avgPartizanship;
        this.avgCorruptability = avgCorruptability;
        this.avgRebelliousness = avgRebelliousness;
    }
}
