using Eppy;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent
{
    public static List<RandomEvent> randomEvents = new List<RandomEvent>();

    public string EventCaption;
    public string EventDescription;
    public int Duration;

    float probability = 0.1f;

    public float GetProbability()
    {
        return probability;
    }

    public void SetProbability(params Tuple<string,double>[] Parameters)
    {
        double weightedTotal = 0;

        foreach (Tuple<string,double> p in Parameters)
        {
            weightedTotal += (double)typeof(float).GetField(p.Item1).GetValue(Country.Instance) * p.Item2;
        }

        probability = ((float) Math.Tanh(weightedTotal) + 1) / 4;
    }

    public RandomEvent(string eventCaption, string eventDescription, params Tuple<Country.ChangeableStats, double>[] effects)
    {
        EventCaption = eventCaption;
        EventDescription = eventDescription;

        foreach (Tuple<Country.ChangeableStats, double> p in effects)
        {
            float val = (float)typeof(float).GetField(p.Item1.ToString()).GetValue(Country.Instance);
            typeof(float).GetField(p.Item1.ToString()).SetValue(Country.Instance, val * p.Item2);
        }
    }
}