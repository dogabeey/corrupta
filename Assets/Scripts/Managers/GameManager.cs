using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour, ISaveable
{
    [InlineEditor]
    public GameConstants gameConstants;
    [InlineEditor]
    public EventManager eventManager;
    [InlineEditor]
    public SaveManager saveManager;
    [InlineEditor]
    public PoolingSystem poolingSystem;

    public List<CityController> cityControllers;
    public List<PersonController> personControllers;
    public List<PartyController> partyControllers;
    public List<MediaController> mediaControllers;

    internal GameInput gameInput;
    internal List<CityDefiniton> cityDefinitions;
    internal List<City> cities;
    internal List<Ideology> ideologies;
    internal List<Media> medias;
    internal List<Person> people;
    internal List<Occupation> occupations;
    internal List<Party> parties;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindAnyObjectByType<GameManager>();
            }
            return instance;
        }
    }

    public string SaveId => "Game_Manager";

    public SaveDataType SaveDataType => SaveDataType.WorldProgression;

    private void Awake()
    {
        gameInput = new GameInput();
        SaveManager.Instance.Register(this);

        InitGame();
    }


    private void InitGame()
    {
        EventManager.Instance.Init();

        LoadInstances();
        InitializeControllers();

        StartAllControllers();
    }

    private void StartAllControllers()
    {
        cityControllers.ForEach(cc => cc.Start());
        personControllers.ForEach(pc => pc.Start());
        partyControllers.ForEach(pc => pc.Start());
        mediaControllers.ForEach(mc => mc.Start());
    }

    private void LoadInstances()
    {
        medias = Media.GetInstances(); // Needs controller. **
        people = Person.GetInstances(); // Needs controller. **
        parties = Party.GetInstances(); // Needs controller. **
        cityDefinitions = CityDefiniton.GetInstances();
        occupations = Occupation.GetInstances();
        cities = City.GetInstances(); // Needs controller. **
        ideologies = Ideology.GetInstances();
    }

    private void InitializeControllers()
    {
        cities.ForEach(c =>
        {
            CityController cityController = new CityController(c, c.mayor, c.governor, c.citizens);
            cityControllers.Add(cityController);
        });
        people.ForEach(p =>
        {
            PersonController personController = new PersonController(p, p.fame, p.personAge, p.corruption, p.ideology, p.baseManagement, p.baseDiplomacy, p.baseWisdom, p.baseSpeech, p.baseIntrigue);
            personControllers.Add(personController);
        });
        parties.ForEach(p =>
        {
            PartyController partyController = new PartyController(p, p.partyName, p.chairPerson, p.viceChairPerson, p.ideology, p.deputyList);
            partyControllers.Add(partyController);
        });
        medias.ForEach(m =>
        {
            MediaController mediaController = new MediaController(m, m.ideology, m.influence);
            mediaControllers.Add(mediaController);
        });

        // Build lookup after controllers are created.
        personControllerById = null;
        EnsurePersonControllerIndex();
    }

    private void Update()
    {
        UpdateAllControllers();

    }

    private void UpdateAllControllers()
    {
        cityControllers.ForEach(cc => cc.Update());
        personControllers.ForEach(pc => pc.Update());
        partyControllers.ForEach(pc => pc.Update());
        mediaControllers.ForEach(mc => mc.Update());
    }

    private void OnDestroy()
    {
        DestroyAllControllers();
    }

    private void OnApplicationQuit()
    {
        if(saveManager != null && saveManager.saveOnQuit)
            saveManager.Save(); 
    }

    private void DestroyAllControllers()
    {
        cityControllers.ForEach(cc => cc.OnDestroy());
        personControllers.ForEach(pc => pc.OnDestroy());
        partyControllers.ForEach(pc => pc.OnDestroy());
        mediaControllers.ForEach(mc => mc.OnDestroy());
    }

    public Dictionary<string, object> Save()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        return saveData;
    }

    public bool Load(System.Action onLoadSuccess = null, System.Action onLoadFail = null)
    {
        JSONNode saveData = SaveManager.Instance.LoadSave(this);
        if (saveData != null)
        {

            onLoadSuccess?.Invoke();
            return true;
        }
        else
        {

            onLoadFail?.Invoke();
            return false;
        }

    }

    private Dictionary<int, PersonController> personControllerById;

    public PersonController GetPersonController(int personId)
    {
        EnsurePersonControllerIndex();
        personControllerById.TryGetValue(personId, out var controller);
        return controller;
    }

    public PersonController GetPersonController(Person person)
    {
        if (person == null) return null;
        return GetPersonController(person.id);
    }

    private void EnsurePersonControllerIndex()
    {
        if (personControllerById != null) return;
        personControllerById = new Dictionary<int, PersonController>();
        if (personControllers == null) return;

        for (int i = 0; i < personControllers.Count; i++)
        {
            var pc = personControllers[i];
            if (pc == null) continue;
            if (personControllerById.ContainsKey(pc.id)) personControllerById[pc.id] = pc;
            else personControllerById.Add(pc.id, pc);
        }
    }
}
