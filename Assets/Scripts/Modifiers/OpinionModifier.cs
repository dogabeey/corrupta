using System;
using System.Collections.Generic;
using UnityEngine;

public class OpinionModifier
{
    public static List<OpinionModifier> opinions = new List<OpinionModifier>();

    public string name;
    public float value;
    public int maxStack;
    public float decayRate;
    public bool isAuto;
    public List<PersonCondition> conditions;

    public OpinionModifier()
    {

    }
    public OpinionModifier(string name, float value, int maxStack, float decayRate, bool isAuto, List<PersonCondition> conditions)
    {
        if (!opinions.Exists(p => p.name == name))
        {
            this.name = name;
        }
        else
        {
            Debug.LogError("A modifier called " + name + " already exists. The object was not created.");
            return;
        }
        this.value = value;
        this.maxStack = maxStack;
        this.decayRate = decayRate;
        this.isAuto = isAuto;
        this.conditions = conditions;
    }
}
