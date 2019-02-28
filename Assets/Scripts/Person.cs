using System;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Person
{
    public static List<Person> people = new List<Person>();
    public static Person player;
    public static int autoNumber = 0;

    bool isPolitician;

    public int uuid = autoNumber;
    public string firstName, lastName;
    public float fame = 0;
    public float corruption = 0;
    string ideology;

    public int management = Mathf.RoundToInt(UnityEngine.Random.value * 10);
    public int diplomacy = Mathf.RoundToInt(UnityEngine.Random.value * 10);
    public int wisdom = Mathf.RoundToInt(UnityEngine.Random.value * 10);
    public int speech = Mathf.RoundToInt(UnityEngine.Random.value * 10);
    public int intrigue = Mathf.RoundToInt(UnityEngine.Random.value * 10);

    public Person()
    {

    }
    public Person(string firstName,string lastName,string ideology, float fame = 0, float corruption = 0,bool isPolitician = true, int uuid = 0)
    {
        if (!people.Exists(p => p.uuid == uuid))
        {
            this.uuid = uuid;
        }
        else
        {
            do uuid = UnityEngine.Random.Range(1, 100000); while (people.Exists(p => p.uuid == uuid));
            Debug.LogWarning("A person with same uuid already exists. Setting uuid to " + uuid + " .");
        }
        this.firstName = firstName;
        this.lastName = lastName;
        this.ideology = ideology;
        this.fame = fame < 0 ? 0 : fame;
        this.corruption = corruption < 0 ? 0 : corruption;
        this.isPolitician = isPolitician;
        autoNumber++;
        people.Add(this);

        //Debug.Log("[" + this.uuid + "] Succesfully added a politican to the country named " + firstName + " " + lastName + ", who follows " + ideology + " ideology. Their fame is " + fame + " and they are " + this.corruption.ToString() + " percent corrupted.");
    }

    public Ideology GetIdeology()
    {
        return Ideology.ideologyList.Find(i => i.ideologyName == ideology);
    }
}
