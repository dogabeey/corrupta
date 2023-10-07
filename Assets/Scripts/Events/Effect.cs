using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Xml.Serialization;

public abstract class PersonEffect
{
    public int defPersonId;
    internal Person person;

    public PersonEffect()
    {

    }
    public virtual void Execute()
    {

    }
}