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

        medias = Media.GetInstances(); // Needs controller. **
        people = Person.GetInstances(); // Needs controller. **
        parties = Party.GetInstances(); // Needs controller. **
        cityDefinitions = CityDefiniton.GetInstances();
        occupations = Occupation.GetInstances();
        cities = City.GetInstances(); // Needs controller. **
        ideologies = Ideology.GetInstances();

        InitializeControllers();

        saveManager.Start();
            

        //Country.InitCountry("Turkey", "Ankara");
    }

    private void InitializeControllers()
    {
        cities.ForEach(c =>
        {
            CityController cityController = new GameObject(c.cityName + "_Controller").AddComponent<CityController>();
            cityController.Init(c);
            cityControllers.Add(cityController);
        });
        people.ForEach(p =>
        {
            PersonController personController = new GameObject(p.firstName + "_" + p.lastName + "_Controller").AddComponent<PersonController>();
            personController.Init(p);
            personControllers.Add(personController);
        });
        parties.ForEach(p =>
        {
            PartyController partyController = new GameObject(p.partyName + "_Controller").AddComponent<PartyController>();
            partyController.Init(p);
            partyControllers.Add(partyController);
        });
        medias.ForEach(m =>
        {
            MediaController mediaController = new GameObject(m.mediaName + "_Controller").AddComponent<MediaController>();
            mediaController.Init(m);
            mediaControllers.Add(mediaController);
        });
    }

    private void Update()
    {
        saveManager.Update();

        cityDefinitions.ForEach(cd => cd.Update());
        cities.ForEach(c => c.Update());
        ideologies.ForEach(i => i.Update());
        medias.ForEach(m => m.Update());
        people.ForEach(p => p.Update());
        occupations.ForEach(o => o.Update());
        parties.ForEach(p => p.Update());
    }
    private void OnDestroy()
    {
        saveManager.OnManagerDestroy();

        cityDefinitions.ForEach(cd => cd.OnManagerDestroy());
        cities.ForEach(c => c.OnManagerDestroy());
        ideologies.ForEach(i => i.OnManagerDestroy());
        medias.ForEach(m => m.OnManagerDestroy());
        people.ForEach(p => p.OnManagerDestroy());
        occupations.ForEach(o => o.OnManagerDestroy());
        parties.ForEach(p => p.OnManagerDestroy());
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
