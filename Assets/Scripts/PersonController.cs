using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PersonController : ObjectController, ISaveable
{
    public Person personSO;
    public float fame;
    public float personAge;
    public float corruption;
    public Ideology ideology;

    public int baseManagement;
    public int baseDiplomacy;
    public int baseWisdom;
    public int baseSpeech;
    public int baseIntrigue;

    public string SaveId => "Person_" + (personSO != null ? personSO.id : id);
    public SaveDataType SaveDataType => SaveDataType.WorldProgression;

    public PersonController(Person personSO, float fame, float personAge, float corruption, Ideology ideology, int baseManagement, int baseDiplomacy, int baseWisdom, int baseSpeech, int baseIntrigue)
    {
        this.personSO = personSO;
        id = personSO != null ? personSO.id : 0;
        this.fame = fame;
        this.personAge = personAge;
        this.corruption = corruption;
        this.ideology = ideology;
        this.baseManagement = baseManagement;
        this.baseDiplomacy = baseDiplomacy;
        this.baseWisdom = baseWisdom;
        this.baseSpeech = baseSpeech;
        this.baseIntrigue = baseIntrigue;
    }

    public override void Start()
    {
        base.Start();
        SaveManager.Instance.Register(this);
        Load();
    }

    public int Management => baseManagement;
    public int Diplomacy => baseDiplomacy;
    public int Wisdom => baseWisdom;
    public int Speech => baseSpeech;
    public int Intrigue => baseIntrigue;

    public void Init(Person personSO)
    {
        this.personSO = personSO;
        id = personSO.id;
        fame = personSO.fame;
        personAge = personSO.personAge;
        corruption = personSO.corruption;
        ideology = personSO.ideology;
        baseManagement = personSO.baseManagement;
        baseDiplomacy = personSO.baseDiplomacy;
        baseWisdom = personSO.baseWisdom;
        baseSpeech = personSO.baseSpeech;
        baseIntrigue = personSO.baseIntrigue;
    }

    public Dictionary<string, object> Save()
    {
        var saveData = new Dictionary<string, object>();

        // Use id for references
        saveData["id"] = personSO != null ? personSO.id : id;

        saveData["fame"] = fame;
        saveData["personAge"] = personAge;
        saveData["corruption"] = corruption;
        saveData["ideology_id"] = ideology != null ? ideology.id : (personSO != null && personSO.ideology != null ? personSO.ideology.id : -1);

        saveData["baseManagement"] = baseManagement;
        saveData["baseDiplomacy"] = baseDiplomacy;
        saveData["baseWisdom"] = baseWisdom;
        saveData["baseSpeech"] = baseSpeech;
        saveData["baseIntrigue"] = baseIntrigue;

        return saveData;
    }

    public bool Load(Action onLoadSuccess = null, Action onLoadFail = null)
    {
        JSONNode loadData = SaveManager.Instance.LoadSave(this);
        if (loadData == null)
        {
            onLoadFail?.Invoke();
            return false;
        }

        int loadedId = loadData["id"].AsInt;
        if (personSO == null || personSO.id != loadedId)
        {
            personSO = Person.GetInstanceByID(loadedId);
            id = loadedId;
        }

        fame = loadData["fame"].AsFloat;
        personAge = loadData["personAge"].AsFloat;
        corruption = loadData["corruption"].AsFloat;

        if (loadData.HasKey("ideology_id"))
        {
            int ideologyId = loadData["ideology_id"].AsInt;
            ideology = Ideology.GetInstanceByID(ideologyId);
        }

        baseManagement = loadData["baseManagement"].AsInt;
        baseDiplomacy = loadData["baseDiplomacy"].AsInt;
        baseWisdom = loadData["baseWisdom"].AsInt;
        baseSpeech = loadData["baseSpeech"].AsInt;
        baseIntrigue = loadData["baseIntrigue"].AsInt;

        onLoadSuccess?.Invoke();
        return true;
    }
}
