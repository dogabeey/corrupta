using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Lionsfall.SimpleJSON;

[System.Serializable]
public class CitizenGroup : ISaveable
{
    public const int POP_PER_GROUP = 100000;

    public Party party;
    public Ideology ideology;
    public Occupation occupation;

    public float wealth;
    public float happiness;
    public float education;

    public string SaveId => "citizen_" + GetHashCode();

    public SaveDataType SaveDataType => throw new System.NotImplementedException();

    public CitizenGroup()
    {
        
    }

    public CitizenGroup(Party party, Ideology ideology, Occupation occupation, float wealth, float happiness, float education)
    {
        this.party = party;
        this.ideology = ideology;
        this.occupation = occupation;
        this.wealth = wealth;
        this.happiness = happiness;
        this.education = education;
    }

    public Dictionary<string, object> Save()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        saveData["party_id"] = party != null ? party.id : -1;
        saveData["ideology_id"] = ideology != null ? ideology.id : -1;
        saveData["occupation_id"] = occupation != null ? occupation.id : -1;

        return saveData;
    }

    public void Load()
    {
        JSONNode loadData = SaveManager.Instance.LoadSave(this);

        int partyId = loadData["party_id"].AsInt;
        if (partyId != -1)
        {
            party = GameManager.Instance.parties.FindAsync(p => p.id == partyId).Result;
        }
        int ideologyId = loadData["ideology_id"].AsInt;
        if (ideologyId != -1)
        {
            ideology = GameManager.Instance.ideologies.FindAsync(i => i.id == ideologyId).Result;
        }
        int occupationId = loadData["occupation_id"].AsInt;
        if (occupationId != -1)
        {
            occupation = GameManager.Instance.occupations.FindAsync(o => o.id == occupationId).Result;
        }
    }
}
