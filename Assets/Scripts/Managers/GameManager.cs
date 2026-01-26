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


    public static GameManager Instance;

    public string SaveId => "Game_Manager";

    public SaveDataType SaveDataType => SaveDataType.WorldProgression;

    private void Awake()
    {
        Instance = this;

        gameInput = new GameInput();
        SaveManager.Instance.Register(this);

        InitGame();
    }


    private void InitGame()
    {
        EventManager.Instance.Init();

        LoadInstances();
        InitializeControllers();

        saveManager.Start();
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
            CityController cityController = new CityController(c, c.mayor, c.citizens);
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
    }

    private void Update()
    {
        saveManager.Update();
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
        saveManager.OnManagerDestroy();
        DestroyAllControllers();
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

}
