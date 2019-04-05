using System;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Person
{
    public enum Title
    {
        President,
        VicePresident,
        ParliamentAdmin,
        Deputy,
        PartyLeader,
        PartyViceLeader,
        Mayor,
        Councilor,
        Advisor,
        Freelance,
    }

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

    public string GetTitleString()
    {
        if (GetTitle() == Title.Advisor) return "Advisor";
        if (GetTitle() == Title.Councilor) return "City Councilor";
        if (GetTitle() == Title.Deputy) return "Deputy";
        if (GetTitle() == Title.Freelance) return "Freelance Politician";
        if (GetTitle() == Title.Mayor) return "Mayor";
        if (GetTitle() == Title.ParliamentAdmin) return "Admin of Parliament";
        if (GetTitle() == Title.PartyLeader) return "Party Leader";
        if (GetTitle() == Title.PartyViceLeader) return "Party Vice Leader";
        if (GetTitle() == Title.VicePresident) return "President of " + Country.Instance.name;
        if (GetTitle() == Title.President) return "President of " + Country.Instance.name;
        else return "absolutely nothing";
    }

    public Ideology GetIdeology()
    {
        return Ideology.ideologyList.Find(i => i.ideologyName == ideology);
    }
    public Title GetTitle()
    {
        if (Government.governmentList.FindLast(g => g.isActive == true).president == this) return Title.President;
        if (Government.governmentList.FindLast(g => g.isActive == true).vicePresident == this) return Title.VicePresident;
        if (Parliament.admin == this) return Title.ParliamentAdmin;
        foreach (Party p in Party.parties)
        {
            if (p.GetChairPerson() == this) return Title.PartyLeader;
        }
        foreach (Party p in Party.parties)
        {
            if (p.GetViceChairPerson() == this) return Title.PartyViceLeader;
        }
        foreach (City c in City.cityList)
        {
            if (c.GetMayor() == this) return Title.Mayor;
        }
        return Title.Freelance;
    }
    public bool HasTitle(object[] args)
    {
        Title arg1 = (Title)args[0];
        return GetTitle() == arg1;
    }
    public bool IsCorruptedMoreThan(float value)
    {
        return corruption > value;
    }
    public bool IsCorruptedLessThan(float value)
    {
        return corruption < value;
    }
    public bool IsManagementMoreThan(int value)
    {
        return management > value;
    }
    public bool IsDiplomacyMoreThan(int value)
    {
        return diplomacy > value;
    }
    public bool IsWisdomMoreThan(int value)
    {
        return wisdom > value;
    }
    public bool IsSpeechMoreThan(int value)
    {
        return speech > value;
    }
    public bool IsIntrigueMoreThan(int value)
    {
        return intrigue > value;
    }
    public bool IsManagementLessThan(int value)
    {
        return management < value;
    }
    public bool IsDiplomacyLessThan(int value)
    {
        return diplomacy < value;
    }
    public bool IsWisdomLessThan(int value)
    {
        return wisdom < value;
    }
    public bool IsSpeechLessThan(int value)
    {
        return speech < value;
    }
    public bool IsIntrigueLessThan(int value)
    {
        return intrigue < value;
    }
}
