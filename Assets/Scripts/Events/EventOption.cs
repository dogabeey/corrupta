using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOption
{
    public string description;
    public PersonEffect effect;
    public EventOption()
    {

    }
    public EventOption(string description, PersonEffect effect = null)
    {
        this.description = description;
        this.effect = effect;
    }
}
