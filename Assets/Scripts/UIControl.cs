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

    public Slider wealth;
    public Slider prestige;
    public Slider stability;
    public Slider happiness;

    string randomlogo;

    // Use this for initialization
    void Start ()
    {
    //    randomlogo = Mathf.RoundToInt((Random.value * 2) + 1).ToString();
    //    Texture2D tex = Resources.Load<Texture2D>("logo/" + randomlogo);
    //    logo.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }
	
	// Update is called once per frame
	void Update ()
    {
        government currentGov = government.governmentList[government.governmentList.Count - 1];

        //governmentName.text = government.governmentList.Count.ToString() + ". government";
        //president.text = "President: " + currentGov.president.firstName + " " + currentGov.president.lastName;
        //vicePresident.text = "Vice President: " + currentGov.vicePresident.firstName + " " + currentGov.vicePresident.lastName;
        //party.text = currentGov.majorityParty.name;

        wealth.value = Country.Instance.GetWealth();
        prestige.value = Country.Instance.GetPrestige();
        stability.value = Country.Instance.GetStability();
        happiness.value = Country.Instance.GetHappiness();
    }
}
