using Lionsfall;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameConstants gameConstants;
    public EventManager eventManager;
    public SaveManager saveManager;

    internal List<CityDefiniton> cityDefinitions;
    internal List<City> cities;
    internal List<Ideology> ideologies;
    internal List<Media> medias;
    internal List<Person> people;
    internal List<Occupation> occupations;
    internal List<Party> parties;


    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        InitGame();
    }
    

    private void InitGame()
    {
        EventManager.instance.Init();

        cityDefinitions = CityDefiniton.GetRuntimeInstances();
        cities = City.GetRuntimeInstances();
        ideologies = Ideology.GetRuntimeInstances();
        medias = Media.GetRuntimeInstances();
        people = Person.GetRuntimeInstances();
        occupations = Occupation.GetRuntimeInstances();
        parties = Party.GetRuntimeInstances();


        Country.InitCountry("Turkey", "stormwind");

        Country.RandomizeAll();
    }
}
