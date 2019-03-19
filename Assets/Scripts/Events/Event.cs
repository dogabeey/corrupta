using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class CountryEvent
{
    public static List<CountryEvent> gameEvents = new List<CountryEvent>();
    public static List<CountryEvent> invokedEvents = new List<CountryEvent>();

    public List<Condition> conditions;
    public List<Effect> effects;
    public List<EventOption> options;

    public string name, header, description;
    public double probability;
    double defProbability;
    int importance = 1; // 0: Don't notify player. 1: Show in up in reports. 2: Notify warning if unread. 3: Don't let player advance turn if not read.
    [XmlIgnore] public bool isRead = false;

    public CountryEvent()
    {

    }
    public CountryEvent(string name, string header, string description, List<Condition> conditions, List<Effect> effects, List<EventOption> options, double probability, int importance = 1)
    {
        this.conditions = conditions;
        this.effects = effects;
        this.options = options;
        this.name = name;
        this.header = header;
        this.description = description;
        this.probability = probability;
        defProbability = probability;
        this.importance = importance;
        gameEvents.Add(this);
    }

    public void Invoke()
    {
        float diceRoll;
        if ((diceRoll = UnityEngine.Random.Range(0f, 1.0f)) > probability)
        {
            Debug.Log("Rolled " + diceRoll + " for " + name + " event and It will not invoke since It's higher than probability, " + probability);
            probability *= 1.5f;
            return;
        }

        Debug.Log("Rolled " + diceRoll + " for " + name + " event and It will invoke since It's lower than probability, " + probability);

        foreach (Condition c in conditions)
        {
            if (c.IsTrue())
            {
                Debug.Log("The condition of '" + c.ToString() + "' is not true, " + name + " will not invoke.");
                return;
            }
        }

        foreach (Effect e in effects)
        {
            Debug.Log("Executing '" + e.ToString() + "' effect of " + name + ".");
            e.Execute();
        }

        probability = defProbability;
        invokedEvents.Add(this);
    }
}
public class CityEvent
{
    public static List<CityEvent> gameEvents = new List<CityEvent>();
    public static List<CityEvent> invokedEvents = new List<CityEvent>();

    public List<Condition> conditions;
    public List<Effect> effects;
    public List<EventOption> options;

    public string name, header, description;
    public double probability;
    double defProbability;
    int importance = 1; // 0: Don't notify player. 1: Show in up in reports. 2: Notify warning if unread. 3: Don't let player advance turn if not read.
    [XmlIgnore] public bool isRead = false;

    public CityEvent()
    {

    }
    public CityEvent(string name, string header, string description, List<Condition> conditions, List<Effect> effects, List<EventOption> options, double probability, int importance = 1)
    {
        this.conditions = conditions;
        this.effects = effects;
        this.options = options;
        this.name = name;
        this.header = header;
        this.description = description;
        this.probability = probability;
        defProbability = probability;
        this.importance = importance;
        gameEvents.Add(this);
    }

    public void Invoke()
    {
        float diceRoll;
        if ((diceRoll = UnityEngine.Random.Range(0f, 1.0f)) > probability)
        {
            Debug.Log("Rolled " + diceRoll + " for " + name + " event and It will not invoke since It's higher than probability, " + probability);
            probability *= 1.5f;
            return;
        }

        Debug.Log("Rolled " + diceRoll + " for " + name + " event and It will invoke since It's lower than probability, " + probability);

        foreach (Condition c in conditions)
        {
            if (c.IsTrue())
            {
                Debug.Log("The condition of '" + c.ToString() + "' is not true, " + name + " will not invoke.");
                return;
            }
        }

        foreach (Effect e in effects)
        {
            Debug.Log("Executing '" + e.ToString() + "' effect of " + name + ".");
            e.Execute();
        }

        probability = defProbability;
        invokedEvents.Add(this);
    }
}
