using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Party
{
    public static List<Party> parties = new List<Party>();

    public string partyName;
    public string logo;
    public string color;

    public Person founder;
    public Person chairPerson;
    public Person viceChairPerson;
    public string ideology;
    public List<Person> deputyList = new List<Person>();
    public List<Person> informalMembers = new List<Person>();

    public Party()
    {

    }

    public Party(string partyName, string logo, string color, Person founder, Person chairPerson, Person viceChairPerson, string ideology)
    {
        this.partyName = partyName;
        this.logo = logo;
        this.color = color;
        this.founder = founder;
        this.chairPerson = chairPerson;
        this.viceChairPerson = viceChairPerson;
        this.ideology = ideology;

        parties.Add(this);
    }

    public Ideology GetIdeology()
    {
        return Ideology.ideologyList.Find(i => i.ideologyName == ideology);
    }
}

