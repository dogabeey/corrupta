using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Text governmentName;
    public Text president;
    public Text vicePresident;
    public Text party;
    public Image logo;

    public Text wealth;
    public Text prestige;
    public Text stability;
    public Text happiness;

    public InputField firstName;
    public InputField lastName;
    public Dropdown ideology;
    public Text idDescription;
    public Dropdown city;
    public Text cityDescription;

    public Animator animator;

    string randomlogo;

    // Use this for initialization
    void Start ()
    {
        //    randomlogo = Mathf.RoundToInt((Random.value * 2) + 1).ToString();
        //    Texture2D tex = Resources.Load<Texture2D>("logo/" + randomlogo);
        //    logo.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        List<string> idList = new List<string>();
        foreach (Ideology i in Ideology.ideologyList)
        {
            idList.Add(i.ideologyName);
        }
        ideology.AddOptions(idList);
        if(idDescription != null) idDescription.text = Ideology.ideologyList.Find(i => i.ideologyName == ideology.options[ideology.value].text).description;

        List<string> cList = new List<string>();
        foreach (City i in City.cityList)
        {
            cList.Add(i.cityName);
        }
        city.AddOptions(cList);
        if (cityDescription != null) cityDescription.text = City.cityList.Find(i => i.cityName == city.options[city.value].text).description;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Government currentGov;
        if(Government.governmentList.Count > 0) currentGov = Government.governmentList[Government.governmentList.Count - 1];

        //governmentName.text = government.governmentList.Count.ToString() + ". government";
        //president.text = "President: " + currentGov.president.firstName + " " + currentGov.president.lastName;
        //vicePresident.text = "Vice President: " + currentGov.vicePresident.firstName + " " + currentGov.vicePresident.lastName;
        //party.text = currentGov.majorityParty.name;

        wealth.text = Mathf.FloorToInt(Country.Instance.GetWealth()).ToString();
        prestige.text = Mathf.FloorToInt(Country.Instance.GetPrestige()).ToString();
        stability.text = Mathf.FloorToInt(Country.Instance.GetStability()).ToString();
        happiness.text = Mathf.FloorToInt(Country.Instance.GetHappiness()).ToString();
    }

    public void ShowIdeologyDescription()
    {
        Debug.Log(ideology.options[ideology.value].text);
        idDescription.text = Ideology.ideologyList.Find(i => i.ideologyName == ideology.options[ideology.value].text).description;
    }

    public void ShowCityDescription()
    {
        Debug.Log(city.options[city.value].text);
        cityDescription.text = City.cityList.Find(i => i.cityName == city.options[city.value].text).description;
    }

    public void CreateCharacter()
    {
        if (firstName.text == "") firstName.animator.SetTrigger("error");
        else if (lastName.text == "") lastName.animator.SetTrigger("error");
        else Person.player = new Person(firstName.text, lastName.text, ideology.options[ideology.value].text);
    }
}
