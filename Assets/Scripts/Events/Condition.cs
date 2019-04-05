using System.Collections;
using System;
using System.Reflection;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

[XmlInclude(typeof(HasTitle))]
public abstract class PersonCondition
{
    public int defPersonId;

    public PersonCondition()
    {
    }
    public virtual bool IsTrue()
    {
        return true;
    }
}

public class HasTitle : PersonCondition
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
        if (personId == 0)
        {
            return Person.people.Find(p => p.uuid == defPersonId).GetTitle() == title;
        }
        else
        {
            return Person.people.Find(p => p.uuid == personId).GetTitle() == title;
        }
    }

    //public override string ToString()
    //{
    //    Person person = Person.people.Find(p => p.uuid == personId);
    //    return person.ToString() + " must be " + person.GetTitleString() +".";
    //}
}