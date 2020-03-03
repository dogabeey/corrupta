using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestInitializer : MonoBehaviour
{
    public int minAge, maxAge;
    public float maxFame;
    public float maxCorruption;

    void Start ()
    {
        Country.InitCountry("Turkey", "stormwind");
        //XmlParse.ParseIdeology("ideology.xml");

        new Ideology(1,"antiauthoritarianism", "A radical philisopy that advocates self-governed societies based on voluntary institutions which denies authority of law and lawmakers.");
        new Ideology(2,"conservatism", "A political belief that favors conservation of cultural and/or religious values and praises traditional view in lawmaking.");
        new Ideology(3,"religionism", "A view that upholds religious laws and determining a state's laws by majority religion in that state.");
        new Ideology(4,"liberalism", "A political view based on liberty and equality which supports civil rights, democracy, secularism, gender equality, internationalism and the freedoms of speech, the press, religion and markets.");
        new Ideology(5,"nationalism", "A political system characterized by promoting the interests of a particular nation particularly with the aim of gaining and maintaining full sovereignty over the group's homeland.");
        new Ideology(6,"enviromentalism", "An ideology regarding concerns for environmental protection and improvement of the health of the environment, particularly as the measure for this health seeks to incorporate the impact of changes to the environment on the planet.");
        new Ideology(7,"socialism", "A social and political system characterised by social ownership and democratic control of the means of production as well as the political theories and movements associated with them.");
        new Ideology(8,"republicanism", "An ideology centered on citizenship in a state organized as a republic under which the people hold popular sovereignty, which upholds that majority's favor is always the best.");
        new Ideology(9,"centrism", "A political view that involves acceptance or support of a balance of a degree of social equality and a degree of social hierarchy.");
        
        new Opinion<Ideology>(Ideology.GetIdeologyByName("antiauthoritarianism"), Ideology.GetIdeologyByName("conservatism"), -65);
        new Opinion<Ideology>(Ideology.GetIdeologyByName("antiauthoritarianism"), Ideology.GetIdeologyByName("religionism"), -44);
        new Opinion<Ideology>(Ideology.GetIdeologyByName("antiauthoritarianism"), Ideology.GetIdeologyByName("liberalism"), 12);
        new Opinion<Ideology>(Ideology.GetIdeologyByName("antiauthoritarianism"), Ideology.GetIdeologyByName("nationalism"), -33);
        new Opinion<Ideology>(Ideology.GetIdeologyByName("antiauthoritarianism"), Ideology.GetIdeologyByName("enviromentalism"), 1);
        new Opinion<Ideology>(Ideology.GetIdeologyByName("antiauthoritarianism"), Ideology.GetIdeologyByName("socialism"), 30);
        new Opinion<Ideology>(Ideology.GetIdeologyByName("antiauthoritarianism"), Ideology.GetIdeologyByName("republicanism"), 2);
        new Opinion<Ideology>(Ideology.GetIdeologyByName("antiauthoritarianism"), Ideology.GetIdeologyByName("centrism"), 0);

        new City(34,"Istanbul", "A city with long history.",
            new Vector2(-210, 134),
            new List<City.IdeologyRate>()
            {
                new City.IdeologyRate("antiauthoritarianism",5),
                new City.IdeologyRate("conservatism",35),
                new City.IdeologyRate("religionism",45),
                new City.IdeologyRate("liberalism",10),
                new City.IdeologyRate("enviromentalism",4),
                new City.IdeologyRate("socialism",2),
                new City.IdeologyRate("centrism",22),
                new City.IdeologyRate("nationalism",15),
                new City.IdeologyRate("republicanism",25)
            }
        );
        new City(35,"Izmir", "Very laik, much atatürk cosplay.",
                    new Vector2(-297, 15),
                    new List<City.IdeologyRate>()
                    {
                new City.IdeologyRate("antiauthoritarianism",5),
                new City.IdeologyRate("conservatism",35),
                new City.IdeologyRate("religionism",45),
                new City.IdeologyRate("liberalism",10),
                new City.IdeologyRate("enviromentalism",4),
                new City.IdeologyRate("socialism",2),
                new City.IdeologyRate("centrism",22),
                new City.IdeologyRate("nationalism",15),
                new City.IdeologyRate("republicanism",25)
                    }
                );
        new City(06,"Ankara", "A capital in desert.",
                    new Vector2(-70, 35),
                    new List<City.IdeologyRate>()
                    {
                new City.IdeologyRate("antiauthoritarianism",5),
                new City.IdeologyRate("conservatism",35),
                new City.IdeologyRate("religionism",45),
                new City.IdeologyRate("liberalism",10),
                new City.IdeologyRate("enviromentalism",4),
                new City.IdeologyRate("socialism",2),
                new City.IdeologyRate("centrism",22),
                new City.IdeologyRate("nationalism",15),
                new City.IdeologyRate("republicanism",25)
                    }
                );

        new Person("Rejeep Tealip", "Manborn", "religionism", 100, 100, true, 31);
        new Person("Binary", "Thunder", "religionism", 90, 90, true, 32);
        new Person("Suleiman", "Totallynotfetöcü", "religionism", 80, 100, true, 33);
        new Person("Hülo", "Something", "religionism", 80, 100, true, 34);
        new Person("Nonfetö", "Götgılı", "religionism", 80, 100, true, 35);
        new Person("Strata", "Derinli K.", "religionism", 80, 100, true, 36);

        Party akp = new Party("Justice and Develoshsjhdjkz", "AKP", "Yellow", 31, 31, 33, "religionism");
        akp.deputyListId.Add(34);
        akp.deputyListId.Add(35);
        akp.deputyListId.Add(36);

        Government gov = new Government(akp, Person.people.Find(p => p.uuid == 31), Person.people.Find(p => p.uuid == 32));

        new PersonEvent(
                "presidental_experience_event",
                "Being a president has its benefits!",
                "Since you are a president, you're talking to many people as your job. Thus, your diplomacy skills are improved.",
                new List<PersonCondition>()
                {
                    new HasTitle(0,Person.Title.Freelance)
                },
                new List<PersonEffect>()
                {
                    new AddDiplomacy(0,1)
                },
                new List<EventOption>()
                {
                },
                0.1
            );


        XmlParse.ExportAll(Ideology.ideologyList);
        XmlParse.ExportAll(Opinion<Ideology>.opinions);
        XmlParse.ExportAll(City.cityList);
        XmlParse.ExportAll(Person.people);
        XmlParse.ExportAll(Party.parties);
        XmlParse.ExportAll(PersonEvent.gameEvents);

        //Person.people = XmlParse.ImportAll<Person>();
        //City.cityList = XmlParse.ImportAll<City>();
        //Party.parties = XmlParse.ImportAll<Party>();

        Country.RandomizeAll();

        foreach (City city in City.cityList)
        {
            Debug.Log("Ideologies of " + city.cityName + ":");
            foreach (City.IdeologyRate ideology in city.ideologyRates)
            {
                Debug.Log(ideology.ideologyString + ":" + ideology.rate);
            }
        }
	}

    public Ideology RandomIdeology()
    {
        int index = UnityEngine.Random.Range(0,Ideology.ideologyList.Count);
        return Ideology.ideologyList[index];
    }

    public City RandomCity()
    {
        int index = UnityEngine.Random.Range(0,City.cityList.Count);
        return City.cityList[index];
    }

    public string RandomName(string fileName)
    {
        string[] nameList = File.ReadAllLines("Assets\\XML\\" + fileName);
        string retVal = nameList[UnityEngine.Random.Range(0,nameList.Length)];
        return retVal;
    }
}
