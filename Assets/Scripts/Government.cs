using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Government
{
    public static List<Government> governmentList = new List<Government>();

    public Party majorityParty;
    public Person president;
    public Person vicePresident;
    public DateTime date;
    public bool isActive;
    
    public float lastElectionVotes;
    public List<Ministry> cabinet = new List<Ministry>();
    /// <summary>
    /// This sub-class represents ministries that connected to government.
    /// </summary>
    public class Ministry
    {
        public string name;
        public Person minister;
        public Country.ChangeableStats areaOfInterest;

        public Ministry(string name, Person minister,Country.ChangeableStats areaOfInterest)
        {
            this.name = name;
            this.minister = minister;
            this.areaOfInterest = areaOfInterest;
        }
    }

    public Government(Party majorityParty, Person president, Person vicePresident)
    {
        this.majorityParty = majorityParty;
        this.president = president;
        this.vicePresident = vicePresident;
        isActive = true;
        foreach (Government gov in governmentList)
        {
            gov.isActive = false;
        }
        //string yt = GameObject.FindGameObjectWithTag("simulation").GetComponent<Simulator>().yearText.text;
        //string mt = GameObject.FindGameObjectWithTag("simulation").GetComponent<Simulator>().monthText.text;

        governmentList.Add(this);
    }
}
