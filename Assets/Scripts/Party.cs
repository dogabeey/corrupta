using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Party : MonoBehaviour
{
    public static List<Party> parties = new List<Party>();

    public string partyName;
    public string logo;

    public Person founder;
    public Person chairPerson;
    public Person viceChairPerson;
    public List<Person> deputyList = new List<Person>();
    public List<Person> informalMembers = new List<Person>();
    public Ideology ideology;

    public Party(string name, string logo, Person founder,Ideology ideology)
    {
        this.partyName = name;
        this.logo = logo;
        this.founder = founder;
        this.ideology = ideology;

        parties.Add(this);
    }
}

