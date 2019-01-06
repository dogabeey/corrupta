using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public delegate bool Con();
public delegate void Effect();

public class PersonEvent : MonoBehaviour
{
    public Person triggeredPerson;
    public float probability;
    public List<Con> cons;
    public List<Effect> effects;

    void ExecuteEvent()
    {
        foreach (Con c in cons)
        {
            if (!c())
            {
                return;
            }
        }
        foreach (Effect e in effects)
        {
            e();
        }
    }
}

