using System;
using System.Collections.Generic;

[Serializable]
public class PartyController : ObjectController
{
    public Party partySO;
    public string partyName;
    public Person chairPerson;
    public Person viceChairPerson;
    public Ideology ideology;
    public List<Person> deputyList;

    public PartyController(Party partySO, string partyName, Person chairPerson, Person viceChairPerson, Ideology ideology, List<Person> deputyList)
    {
        this.partySO = partySO;
        this.partyName = partyName;
        this.chairPerson = chairPerson;
        this.viceChairPerson = viceChairPerson;
        this.ideology = ideology;
        this.deputyList = deputyList;
    }

    public void Init(Party partySO)
    {
        this.partySO = partySO;
        partyName = partySO.partyName;
        chairPerson = partySO.chairPerson;
        viceChairPerson = partySO.viceChairPerson; 
        ideology = partySO.ideology;
        deputyList = partySO.deputyList;
    }
}

