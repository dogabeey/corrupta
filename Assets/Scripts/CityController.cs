using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IController
{
    void Start();
    void Update();
    void OnDestroy();
    
}
public class ObjectController : IController
{
    public int id;
    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }
    public virtual void OnDestroy()
    {

    }
}

[Serializable]
public class CityController : ObjectController, ISaveable
{
    public City citySO;
    public Person mayor;
    public Person governor;
    public List<CitizenGroup> citizens;

    public string SaveId => "City_" + citySO.id;
    public SaveDataType SaveDataType => SaveDataType.WorldProgression;

    public CityController(City citySO, Person mayor, Person governor, List<CitizenGroup> citizens)
    {
        this.citySO = citySO;
        id = citySO.id;
        this.mayor = mayor;
        this.governor = governor;
        this.citizens = citizens;
    }

    public int Population => citizens.Count * CitizenGroup.POP_PER_GROUP;

    public void Init(City citySO)
    {
        this.citySO = citySO;
        mayor = citySO.mayor;
        governor = citySO.governor;
        citizens = citySO.citizens;
    }

    public override void Start()
    {
        base.Start();
        SaveManager.Instance.Register(this);
        Load(OnLoadSuccess, OnLoadFail);
    }


    public Dictionary<string, object> Save()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        saveData["id"] = citySO.id;
        if (mayor) saveData["mayor_id"] = mayor.id;
        if (governor) saveData["governor_id"] = governor.id;
        saveData["citizen_count"] = citizens.Count;
        for (int i = 0; i < citizens.Count; i++)
        {
            saveData[$"citizens/citizen_{i}/party_id"] = citizens[i].party != null ? citizens[i].party.id : -1;
            saveData[$"citizens/citizen_{i}/ideology_id"] = citizens[i].ideology != null ? citizens[i].ideology.id : -1;
            saveData[$"citizens/citizen_{i}/occupation_id"] = citizens[i].occupation != null ? citizens[i].occupation.id : -1;
        }

        return saveData;
    }

    public bool Load(Action onLoadSuccess = null, Action onLoadFail = null)
    {
        JSONNode loadData = SaveManager.Instance.LoadSave(this);
        if (loadData != null)
        {
            citySO = City.GetInstanceByID(loadData["id"].AsInt);
            if (loadData.HasKey("mayor_id"))
            {
                int mayorId = loadData["mayor_id"].AsInt;
                mayor = GameManager.Instance.GetPersonController(mayorId)?.personSO
                    ?? GameManager.Instance.people.FirstOrDefault(p => p.id == mayorId);
                if (!mayor)
                {
                    Debug.LogWarning($"[CityController] Mayor with ID {mayorId} not found for City ID {citySO.id}. Reverting to default mayor.");
                    mayor = citySO.mayor;
                }
            }
            else
            {
                Debug.LogWarning("[CityController] No mayor_id found in save data. Reverting to default mayor.");
            }

            if (loadData.HasKey("governor_id"))
            {
                int governorId = loadData["governor_id"].AsInt;
                governor = GameManager.Instance.GetPersonController(governorId)?.personSO
                    ?? GameManager.Instance.people.FirstOrDefault(p => p.id == governorId);
                if (!governor)
                {
                    Debug.LogWarning($"[CityController] Governor with ID {governorId} not found for City ID {citySO.id}. Reverting to default governor.");
                    governor = citySO.governor;
                }
            }
            else
            {
                Debug.LogWarning("[CityController] No governor_id found in save data. Reverting to default governor.");
            }

            int citizenCount = loadData["citizen_count"];
            for (int i = 0; i < citizenCount; i++)
            {
                int partyId = loadData[$"citizens/citizen_{i}/party_id"].AsInt;
                int ideologyId = loadData[$"citizens/citizen_{i}/ideology_id"].AsInt;
                int occupationId = loadData[$"citizens/citizen_{i}/occupation_id"].AsInt;


                Party party = Party.GetInstanceByID(partyId);
                Ideology ideology = Ideology.GetInstanceByID(ideologyId);
                Occupation occupation = Occupation.GetInstanceByID(occupationId);

                CitizenGroup citizenGroup = new CitizenGroup();
                citizenGroup.party = party;
                citizenGroup.ideology = ideology;
                citizenGroup.occupation = occupation;
                citizens.Add(citizenGroup);
            }

            onLoadSuccess?.Invoke();
            return true;
        }
        else
        {
            onLoadFail?.Invoke();
            return false;
        }
    }
    private void OnLoadSuccess()
    {

    }
    private void OnLoadFail()
    {
    }
}
