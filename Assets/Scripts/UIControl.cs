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
    public Sprite logo;

    public Text wealth;
    public Text prestige;
    public Text stability;
    public Text happiness;

    string randomlogo;

    // Use this for initialization
    void Start ()
    {
        randomlogo = Mathf.RoundToInt(Random.value * 3).ToString();
        Debug.Log(randomlogo);
        Debug.Log(Resources.Load<Sprite>("logo/1"));
        logo = Resources.Load<Sprite>("logo/1");
    }
	
	// Update is called once per frame
	void Update ()
    {
        government currentGov = government.governmentList[government.governmentList.Count - 1];

        governmentName.text = government.governmentList.Count.ToString() + ". government";
        president.text = "President: " + currentGov.president.firstName + " " + currentGov.president.lastName;
        vicePresident.text = "Vice President: " + currentGov.vicePresident.firstName + " " + currentGov.vicePresident.lastName;
        party.text = currentGov.majorityParty.name;

        wealth.text = Country.Instance.GetWealth().ToString();
        prestige.text = Country.Instance.GetPrestige().ToString();
        stability.text = Country.Instance.GetStability().ToString();
        happiness.text = Country.Instance.GetHappiness().ToString();
    }
}
