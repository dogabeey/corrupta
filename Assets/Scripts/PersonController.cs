using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PersonController : ObjectController, ISaveable
{
    [Header("Person Info")]
    public Person personSO;
    public float fame;
    public float personAge;
    public float corruption;
    public Ideology ideology;
    [Header("Base Stats")]
    public int baseManagement;
    public int baseDiplomacy;
    public int baseWisdom;
    public int baseSpeech;
    public int baseIntrigue;
    [Header("Effects")]
    public List<TimedBonusEffect> activeBonusEffects = new List<TimedBonusEffect>();

    [System.Serializable]
    public class TimedBonusEffect
    {
        public int remainingTurns;
        public BonusEffect effect;
    }

    // Advisors currently assigned to this person.
    // Saved/loaded by advisor name (since advisors are runtime-created, non-ScriptableObject entries).
    [NonSerialized]
    private readonly List<AdvisorBase> advisors = new List<AdvisorBase>();
    public IReadOnlyList<AdvisorBase> Advisors => advisors;

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

        advisors.Clear();
    }

    public void AddAdvisor(AdvisorBase advisor)
    {
        if (advisor == null) return;
        if (advisors.Contains(advisor)) return;
        advisors.Add(advisor);
    }

    public bool RemoveAdvisor(AdvisorBase advisor)
    {
        if (advisor == null) return false;
        return advisors.Remove(advisor);
    }

    public void ClearAdvisors()
    {
        advisors.Clear();
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

        // Advisors
        int advisorCount = advisors != null ? advisors.Count : 0;
        saveData["advisors/count"] = advisorCount;
        for (int i = 0; i < advisorCount; i++)
        {
            // AdvisorBase is not a ScriptableObject, so persist by stable key.
            // Currently advisor name is the only available unique-ish identifier.
            saveData[$"advisors/{i}/name"] = advisors[i] != null ? advisors[i].AdvisorName : string.Empty;
        }

        // Active bonus effects
        int bonusCount = activeBonusEffects != null ? activeBonusEffects.Count : 0;
        saveData["activeBonusEffects/count"] = bonusCount;
        for (int i = 0; i < bonusCount; i++)
        {
            var t = activeBonusEffects[i];
            saveData[$"activeBonusEffects/{i}/remainingTurns"] = t != null ? t.remainingTurns : 0;

            var e = t != null ? t.effect : default;
            saveData[$"activeBonusEffects/{i}/target"] = (int)e.target;
            saveData[$"activeBonusEffects/{i}/operation"] = (int)e.operation;
            saveData[$"activeBonusEffects/{i}/value"] = e.value;
            saveData[$"activeBonusEffects/{i}/scope"] = (int)e.scope;
            saveData[$"activeBonusEffects/{i}/bonusEffectCostMultiplier"] = e.bonusEffectCostMultiplier;
            saveData[$"activeBonusEffects/{i}/customLabel"] = e.customLabel ?? string.Empty;

            // Optional filters (persist by id; -1 means null)
            saveData[$"activeBonusEffects/{i}/occupation_id"] = e.occupation != null ? e.occupation.id : -1;
            saveData[$"activeBonusEffects/{i}/ideology_id"] = e.ideology != null ? e.ideology.id : -1;
            saveData[$"activeBonusEffects/{i}/city_id"] = e.city != null ? e.city.id : -1;
        }

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

        // Advisors
        advisors.Clear();
        int advisorCount = loadData.HasKey("advisors/count") ? loadData["advisors/count"].AsInt : 0;
        for (int i = 0; i < advisorCount; i++)
        {
            string advisorName = loadData.HasKey($"advisors/{i}/name") ? loadData[$"advisors/{i}/name"].Value : null;
            if (string.IsNullOrEmpty(advisorName)) continue;

            AdvisorBase advisor = null;
            if (GameManager.Instance != null && GameManager.Instance.advisorPool != null && GameManager.Instance.advisorPool.advisors != null)
                advisor = GameManager.Instance.advisorPool.advisors.Find(a => a != null && a.AdvisorName == advisorName);

            if (advisor != null)
                advisors.Add(advisor);
        }

        // Active bonus effects
        if (activeBonusEffects == null) activeBonusEffects = new List<TimedBonusEffect>();
        activeBonusEffects.Clear();
        int bonusCount = loadData.HasKey("activeBonusEffects/count") ? loadData["activeBonusEffects/count"].AsInt : 0;
        for (int i = 0; i < bonusCount; i++)
        {
            int remainingTurns = loadData.HasKey($"activeBonusEffects/{i}/remainingTurns")
                ? loadData[$"activeBonusEffects/{i}/remainingTurns"].AsInt
                : 0;

            BonusEffect effect = new BonusEffect();
            if (loadData.HasKey($"activeBonusEffects/{i}/target")) effect.target = (BonusStat)loadData[$"activeBonusEffects/{i}/target"].AsInt;
            if (loadData.HasKey($"activeBonusEffects/{i}/operation")) effect.operation = (BonusOperation)loadData[$"activeBonusEffects/{i}/operation"].AsInt;
            if (loadData.HasKey($"activeBonusEffects/{i}/value")) effect.value = loadData[$"activeBonusEffects/{i}/value"].AsFloat;
            if (loadData.HasKey($"activeBonusEffects/{i}/scope")) effect.scope = (BonusTargetScope)loadData[$"activeBonusEffects/{i}/scope"].AsInt;
            if (loadData.HasKey($"activeBonusEffects/{i}/bonusEffectCostMultiplier")) effect.bonusEffectCostMultiplier = loadData[$"activeBonusEffects/{i}/bonusEffectCostMultiplier"].AsFloat;
            if (loadData.HasKey($"activeBonusEffects/{i}/customLabel")) effect.customLabel = loadData[$"activeBonusEffects/{i}/customLabel"].Value;

            int occupationId = loadData.HasKey($"activeBonusEffects/{i}/occupation_id") ? loadData[$"activeBonusEffects/{i}/occupation_id"].AsInt : -1;
            int ideologyId = loadData.HasKey($"activeBonusEffects/{i}/ideology_id") ? loadData[$"activeBonusEffects/{i}/ideology_id"].AsInt : -1;
            int cityId = loadData.HasKey($"activeBonusEffects/{i}/city_id") ? loadData[$"activeBonusEffects/{i}/city_id"].AsInt : -1;

            effect.occupation = occupationId >= 0 ? Occupation.GetInstanceByID(occupationId) : null;
            effect.ideology = ideologyId >= 0 ? Ideology.GetInstanceByID(ideologyId) : null;
            effect.city = cityId >= 0 ? City.GetInstanceByID(cityId) : null;

            activeBonusEffects.Add(new TimedBonusEffect
            {
                remainingTurns = remainingTurns,
                effect = effect
            });
        }

        onLoadSuccess?.Invoke();
        return true;
    }
}
