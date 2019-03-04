using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public static List<Event> gameEvents = new List<Event>();

    public List<Condition> conditions;
    public List<Effect> effects;
    public List<EventOption> options;

    public string name, header, description;
    public double probability;

    public Event()
    {

    }
    public Event(string name, string header, string description, List<Condition> conditions, List<Effect> effects, List<EventOption> options, double probability)
    {
        this.conditions = conditions;
        this.effects = effects;
        this.options = options;
        this.name = name;
        this.header = header;
        this.description = description;
        this.probability = probability;

        gameEvents.Add(this);
    }

    public void Invoke()
    {
        if (Random.Range(0, 1) > System.Math.Tanh(probability))
        {
            
            probability *= 2;
            return;
        }

        foreach (Condition c in conditions)
        {
            if (!c.IsTrue())
            {
                Debug.Log("The condition of '" + c.ToString() + " is not true.");
                return;
            }
        }
        foreach (Effect e in effects)
        {
            e.Execute();
        }
    }
}
