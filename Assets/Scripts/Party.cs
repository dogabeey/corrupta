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

    public int id;
    public int founderId;
    public int chairPersonId;
    public int viceChairPersonId;
    public string ideologyString;

    public Person Founder { get => Person.people.Find(p => p.uuid == founderId); }
    public Person ChairPerson { get => Person.people.Find(p => p.uuid == chairPersonId); }
    public Person ViceChairPerson { get => Person.people.Find(p => p.uuid == viceChairPersonId); }
    public Ideology Ideology { get => Ideology.ideologyList.Find(i => i.ideologyName == ideologyString); }

    public List<int> deputyListId = new List<int>();
    public List<int> informalMembersId = new List<int>();

    public Party()
    {

    }

    public Party(string partyName, string logo, string color, int founderId, int chairPersonId, int viceChairPersonId, string ideology)
    {
        this.partyName = partyName;
        this.logo = logo;
        this.color = color;
        this.founderId = founderId;
        chairPerson = 
        this.chairPersonId = chairPersonId;
        viceChairPerson = 
        this.viceChairPersonId = viceChairPersonId;
        this.ideology = 

        parties.Add(this);
    }

    public List<Person> GetDeputies()
    {
        List<Person> retVal = new List<Person>();
        foreach (int i in deputyListId)
        {
            retVal.Add(Person.people.Find(p => p.uuid == i));
        }
        return retVal;
    }
    public Person GetChairPerson()
    {
        return Person.people.Find(p => p.uuid == chairPersonId);
    }
    public Person GetViceChairPerson()
    {
        return Person.people.Find(p => p.uuid == viceChairPersonId);
    }
    public List<Person> GetInformalMembers()
    {
        List<Person> retVal = new List<Person>();
        foreach (int i in informalMembersId)
        {
            retVal.Add(Person.people.Find(p => p.uuid == i));
        }
        return retVal;
    }
}

