using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOption
{
    public string description;
    public Effect effect;
    public EventOption()
    {

    }
    public EventOption(string description, Effect effect = null)
    {
        this.description = description;
        this.effect = effect;
    }
}
