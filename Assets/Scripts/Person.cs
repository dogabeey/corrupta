using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Person", menuName = "Corrupta/New Person...")]
public class Person : ListedScriptableObject<Person>
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

    public string firstName, lastName;
    public float fame = 0;
    public float corruption = 0;
    public Ideology ideology;


    public int management;
    public int diplomacy;
    public int wisdom;
    public int speech;
    public int intrigue;

    public Person()
    {
    }
    public override void Start()
    {
    }
    public override void Update()
    {
    }
    public override void OnManagerDestroy()
    {
    }

    public void CreateNewAssetFromSO()
    {
        string assetPathAndName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath($"Assets/Resources/ScriptableObjects/People/New Person.asset");
        UnityEditor.AssetDatabase.CreateAsset(this, assetPathAndName);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
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

    public Title GetTitle()
    {
        if (Government.governmentList.FindLast(g => g.isActive == true).president == this) return Title.President;
        if (Government.governmentList.FindLast(g => g.isActive == true).vicePresident == this) return Title.VicePresident;
        if (Parliament.admin == this) return Title.ParliamentAdmin;
        foreach (Party p in Party.parties)
        {
            if (p.chairPerson == this) return Title.PartyLeader;
        }
        foreach (Party p in Party.parties)
        {
            if (p.viceChairPerson == this) return Title.PartyViceLeader;
        }
        foreach (City c in GameManager.Instance.cities)
        {
            if (c.mayor == this) return Title.Mayor;
        }
        return Title.Freelance;
    }
}
