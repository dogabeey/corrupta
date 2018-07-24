using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Party : MonoBehaviour
{
    static List<Party> parties = new List<Party>();

    public string name;
    public string logo;

    public Person founder;
    public Person chairPerson;
    public Person[] viceChairPerson;
    public Person[] deputyList;
    public Person[] informalMembers;
    public Ideology ideology;

    public Party(string name, string logo, Person founder,Ideology ideology)
    {
        this.name = name;
        this.logo = logo;
        this.founder = founder;
        this.ideology = ideology;

        parties.Add(this);
    }
}

