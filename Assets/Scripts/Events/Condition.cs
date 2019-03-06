using System.Collections;
using System;
using System.Reflection;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

[XmlInclude(typeof(HasCountryStat))]
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

public class HasCountryStat : Condition
{
    public Country.ChangeableStats stat;
    public float minValue;

    public HasCountryStat()
    {

    }
    public HasCountryStat(Country.ChangeableStats stat, float minValue)
    {
        this.stat = stat;
        this.minValue = minValue;
    }

    public override bool IsTrue()
    {
        FieldInfo field = typeof(Country).GetField(stat.ToString());
        float value = (float)field.GetValue(Country.Instance);
        return minValue <= value;
    }

    public override string ToString()
    {
        FieldInfo field = typeof(Country).GetField(stat.ToString());
        float value = (float)field.GetValue(Country.Instance);
        return stat.ToString() + " value of the country must be equal or greater than " + minValue + ". (Currently " + value + ").";
    }
}

public class HasTitle : Condition
{
    public int personId;
    public Person.Title title;

    public HasTitle()
    {
    }

    public HasTitle(int personId, Person.Title title)
    {
        this.personId = personId;
        this.title = title;
    }

    public override bool IsTrue()
    {
        return Person.people.Find(p => p.uuid == personId).GetTitle() == title;
    }

    public override string ToString()
    {
        Person person = Person.people.Find(p => p.uuid == personId);
        return person.ToString() + " must be " + person.GetTitleString() +".";
    }
}