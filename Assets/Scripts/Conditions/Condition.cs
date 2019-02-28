using System.Collections;
using System;
using System.Reflection;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

[XmlInclude(typeof(HasStat))]
public abstract class Condition
{
    public Condition()
    {

    }
    public virtual bool IsTrue()
    {
        return true;
    }
}

public class HasStat : Condition
{
    Country.ChangeableStats stat;
    float minValue;

    public override bool IsTrue()
    {
        FieldInfo field = typeof(Country).GetField(stat.ToString());
        float value = (float)field.GetValue(this);
        return minValue > value;
    }
}