using Lionsfall;
using Lionsfall.SimpleJSON;
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

        SaveManager.Instance.Register(this);

        InitGame();
    }
    

    private void InitGame()
    {
        EventManager.Instance.Init();

        cityDefinitions = CityDefiniton.GetRuntimeInstances();
        cities = City.GetRuntimeInstances();
        ideologies = Ideology.GetRuntimeInstances();
        medias = Media.GetRuntimeInstances();
        people = Person.GetRuntimeInstances();
        occupations = Occupation.GetRuntimeInstances();
        parties = Party.GetRuntimeInstances();


        Country.InitCountry("Turkey", "Ankara");

        Country.RandomizeAll();
    }

    public Dictionary<string, object> Save()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        return saveData;
    }

    public void Load()
    {
        JSONNode saveData = SaveManager.Instance.LoadSave(this);

    }

}
