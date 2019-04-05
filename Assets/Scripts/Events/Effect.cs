using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Xml.Serialization;

// Her yeni efekt için bunları eklemek gerekiyor yoksa xmle dönüştürülemiyor
[XmlInclude(typeof(AddDiplomacy))]
public abstract class PersonEffect
{
    public int defPersonId;
    internal Person person;

    public PersonEffect()
    {

    }
    public virtual void Execute()
    {

    }
}

public class AddDiplomacy : PersonEffect
{
    public int personId;
    public int value;

    public AddDiplomacy()
    {

    }
    public AddDiplomacy(int personId, int value)
    {
        this.personId = personId;
        this.value = value;
    }

    public override void Execute()
    {
        if (personId == 0)
        {
            person = Person.people.Find(p => p.uuid == defPersonId);
            person.diplomacy += value;
        }
        else
        {
            person = Person.people.Find(p => p.uuid == personId);
            person.diplomacy += value;
        }
    }

    public override string ToString()
    {
        return "Diplomacy skill of " + person + " is increased by " + value + "."; 
    }
}

public class AddSpeech : PersonEffect
{
    public int personId;
    public int value;

    public AddSpeech()
    {

    }
    public AddSpeech(int personId, int value)
    {
        this.personId = personId;
        this.value = value;
    }

    public override void Execute()
    {
        if (personId == 0)
        {
            person = Person.people.Find(p => p.uuid == defPersonId);
            person.speech += value;
        }
        else
        {
            person = Person.people.Find(p => p.uuid == personId);
            person.speech += value;
        }
    }

    public override string ToString()
    {
        return "Diplomacy skill of " + person + " is increased by " + value + "."; 
    }
}
