using System;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Person : MonoBehaviour
{
    public static List<Person> people = new List<Person>();

    bool isPolitician;
    public string firstName, lastName;
    public DateTime birthDate;
    public float fame = 0;
    public float corruption = 0;

    public Ideology ideology;

    public float Management = UnityEngine.Random.value * 10;
    public float Diplomacy = UnityEngine.Random.value * 10;
    public float Wisdom = UnityEngine.Random.value * 10;
    public float Speech = UnityEngine.Random.value * 10;
    public float Intrigue = UnityEngine.Random.value * 10;

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
