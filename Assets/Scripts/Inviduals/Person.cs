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

    bool isPolitician;

    public string firstName, lastName;
    public DateTime birthDate;
    public float fame = 0;
    public float corruption = 0;

    public Ideology ideology;

    public int Management = Mathf.RoundToInt(UnityEngine.Random.value * 10);
    public int Diplomacy = Mathf.RoundToInt(UnityEngine.Random.value * 10);
    public int Wisdom = Mathf.RoundToInt(UnityEngine.Random.value * 10);
    public int Speech = Mathf.RoundToInt(UnityEngine.Random.value * 10);
    public int Intrigue = Mathf.RoundToInt(UnityEngine.Random.value * 10);

    public Person(string firstName,string lastName,Ideology ideology, float fame = 0, float corruption = 0,bool isPolitician = true)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.ideology = ideology;
        this.fame = fame;
        this.corruption = corruption;
        this.isPolitician = isPolitician;

        people.Add(this);

        
    }
}
