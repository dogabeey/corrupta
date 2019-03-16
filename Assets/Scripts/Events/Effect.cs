using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Xml.Serialization;

// Her yeni efekt için bunları eklemek gerekiyor yoksa xmle dönüştürülemiyor
[XmlInclude(typeof(ExecuteEventByName))]
[XmlInclude(typeof(SetCountryStat))]
[XmlInclude(typeof(AddCountryStat))]
public abstract class Effect
{
    public Effect()
    {

    }
    public virtual void Execute()
    {

    }
}
public class ExecuteEventByName : Effect
{
    public string eventName;
    public ExecuteEventByName()
    {

    }
    public ExecuteEventByName(string eventName)
    {
        this.eventName = eventName;
    }

    public override void Execute()
    {
        List<Effect> effects = RandomEvent.gameEvents.Find(e => e.name == eventName).effects;
        foreach (Effect e in effects)
        {
            e.Execute();
        }
    }

    public override string ToString()
    {
        return eventName + " event happens.";
    }
}

public class SetCountryStat : Effect
{
    Country.ChangeableStats stat;
    float value;
    public SetCountryStat()
    {

    }
    public SetCountryStat(Country.ChangeableStats stat, float value)
    {
        this.stat = stat;
        this.value = value;
    }

    public override void Execute()
    {
        FieldInfo field = typeof(Country).GetField(stat.ToString());
        field.SetValue(Country.Instance, value);
    }

    public override string ToString()
    {
        return "Set " + stat.ToString() + " value to " + value + ".";
    }
}

public class AddCountryStat : Effect
{
    Country.ChangeableStats stat;
    float value;
    public AddCountryStat()
    {

    }
    public AddCountryStat(Country.ChangeableStats stat, float value)
    {
        this.stat = stat;
        this.value = value;
    }

    public override void Execute()
    {
        FieldInfo field = typeof(Country).GetField(stat.ToString());
        field.SetValue(Country.Instance, (float)field.GetValue(Country.Instance) + value);
    }

    public override string ToString()
    {
        return stat.ToString() + " Value of " + stat.ToString() + (value >= 0 ? "is increased" : "is decreased" ) + " by " + value + ".";
    }
}