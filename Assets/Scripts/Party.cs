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

    public int founderId;
    public int chairPersonId;
    public int viceChairPersonId;
    public string ideologyString;

    Person founder;
    Person chairPerson;
    Person viceChairPerson;
    Ideology ideology;

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
        founder = Person.people.Find(p => p.uuid == founderId);
        this.founderId = founderId;
        chairPerson = Person.people.Find(p => p.uuid == chairPersonId);
        this.chairPersonId = chairPersonId;
        viceChairPerson = Person.people.Find(p => p.uuid == viceChairPersonId);
        this.viceChairPersonId = viceChairPersonId;
        this.ideology = Ideology.ideologyList.Find(i => i.ideologyName == ideology);

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

