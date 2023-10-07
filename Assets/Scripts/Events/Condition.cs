using System.Collections;
using System;
using System.Reflection;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;


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