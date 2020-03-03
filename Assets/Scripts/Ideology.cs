using System.Collections;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class Ideology
{
    public static List<Ideology> ideologyList = new List<Ideology>();

    public int id;
    public string ideologyName;
    public string description;

    public Ideology()
    {

    }

    public Ideology(int id, string ideologyName, string description)
    {
        this.id = id;
        this.ideologyName = ideologyName;
        this.description = description;
        ideologyList.Add(this);
    }

    public static Ideology GetIdeologyByName(string id)
    {
        return ideologyList.Find(i => i.ideologyName == id);
    }
}
