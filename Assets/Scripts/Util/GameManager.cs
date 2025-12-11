using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameConstants gameConstants;
    public EventManager eventManager;
    public List<CityDefiniton> cityDefinitions;
    public List<City> cities;
    public List<Ideology> ideologies;
    public List<Media> medias;
    public List<Person> people;
    public List<Occupation> occupations;
    public List<Party> parties;


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
