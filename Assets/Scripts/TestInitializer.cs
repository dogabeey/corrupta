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
        XmlParse.ParseIdeology("ideology.xml");

        new City("Istanbul", "A city with long history and longer line of awaiting immigrants who wants to enter.",
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
        new City("Izmir", "Very laik, no akp here.",
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
        new City("Ankara", "A capital in desert.",
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

        new Person("Rejeep Tealip", "Manborn", "religionism", 100, 100, true,31);
        new Person("Binary", "Thunder", "religionism", 90, 90, true,32);
        new Person("Suleiman", "Totallynotfetöcü", "religionism", 80, 100, true, 33);
        new Person("Hülo", "Something", "religionism", 80, 100, true, 34);
        new Person("Nonfetö", "Götgılı", "religionism", 80, 100, true, 35);
        new Person("Strata", "Derinli", "religionism", 80, 100, true, 36);

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
           

        XmlParse.ExportAll(City.cityList);
        XmlParse.ExportAll(Person.people);
        XmlParse.ExportAll(Party.parties);
        XmlParse.ExportAll(PersonEvent.gameEvents);

        //XmlParse.ImportAll<Person>();
        //XmlParse.ImportAll<City>();
        //XmlParse.ImportAll<Party>();

        Country.RandomizeAll();
        GameObject.FindGameObjectWithTag("uicontroller").SetActive(true);
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
