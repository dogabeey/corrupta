﻿using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opinion<T>
{
    public static List<Opinion<T>> opinions = new List<Opinion<T>>();

    public string individual;
    public string target;
    public float opinionValue;

    public Opinion()
    {

    }

    public Opinion(T individual, T target, float val = 0)
    {
        //individual = opinions.Find(o => individual);
        //target = t;
        //opinionValue = val;

        // new Opinion<T>(t, i, val); // bir opinion oluşturulduğunda karşıdaki insanın da diğerine opinionu oluşuyor.
        //opinions.Add(this);
    }

    public static void SetOpinion(T i, T t, float val)
    {
        opinions.Find(o => o.individual.ToString() == i.ToString() && o.target.ToString() == t.ToString()).opinionValue = val;
    }

    public static float GetOpinion(Person i, Person t)
    {
        float finalValue = 0;
        List<OpinionModifier> activeModifiers = new List<OpinionModifier>();
        if (typeof(T) != typeof(Person))
        {
            Debug.LogError("Only person classes have dynamic opinions to each other");
            return 0;
        }

        foreach (OpinionModifier o in OpinionModifier.opinions)
        {
            if (!o.isAuto) continue;
            bool allTrue = true;
            foreach (PersonCondition c in o.conditions) // Checks every condition and determines if all of them meets criteras.
            {
                if (!c.IsTrue())
                { 
                    allTrue = false;
                    break;
                }
            }
            if (allTrue == true) activeModifiers.Add(o); // If It's true, adds it to active modifier list.
        }
        foreach (OpinionModifier o in activeModifiers)
        {
            finalValue += o.value; // We add every confirmed modifier to the final value.
        }

        return finalValue;
    }

    //public static T GetOpposite(T i)
    //{
    //    List<Opinion<T>> ops = opinions.FindAll(o => o.individual.ToString() == i.ToString());
    //    List<float> opValues = new List<float>();
    //    foreach (Opinion<T> o in ops)
    //    { 
    //        opValues.Add(o.opinionValue);
    //    }

    //    return opinions.Find(o2 => o2.opinionValue == opValues.Min()).target;
    //}
}