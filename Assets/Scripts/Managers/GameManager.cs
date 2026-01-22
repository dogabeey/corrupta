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

        medias = new List<Media>();
        people = new List<Person>();
        occupations = new List<Occupation>();
        parties = new List<Party>();

        cityDefinitions = CityDefiniton.CreateRuntimeInstancesFromAssets();
        cities = City.CreateRuntimeInstancesFromAssets();
        ideologies = Ideology.CreateRuntimeInstancesFromAssets();
        if (!Load())
        { 
            // Medias, people, occupations and parties can be removed/added dynamically. We should always load them from the save data and only load from assets if there is no save data.
            medias = Media.CreateRuntimeInstancesFromAssets();
            people = Person.CreateRuntimeInstancesFromAssets();
            occupations = Occupation.CreateRuntimeInstancesFromAssets();
            parties = Party.CreateRuntimeInstancesFromAssets();
        }
        

        saveManager.Start();

        cityDefinitions.ForEach(cd => cd.Start());
        cities.ForEach(c => c.Start());
        ideologies.ForEach(i => i.Start());
        medias.ForEach(m => m.Start());
        people.ForEach(p => p.Start());
        occupations.ForEach(o => o.Start());
        parties.ForEach(p => p.Start());

        Country.InitCountry("Turkey", "Ankara");

        Country.RandomizeAll();
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
