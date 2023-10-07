using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParliamentUI : MonoBehaviour
{
    public GameObject chairGroup;
    public GameObject chairPrefab;

    public void Awake()
    {
        for (int i = 0; i < chairGroup.transform.childCount; i++)
        {
            Destroy(chairGroup.transform.GetChild(i));
        }
        Debug.Log("Parliament UI started");
        //nameText = GetComponentsInChildren<Text>()[0];
        //partyText = GetComponentsInChildren<Text>()[1];

        //nameText.text = deputy.firstName + deputy.lastName;
        //partyText.text = Party.parties.Find(pt => pt.deputyList.Find(pr => pr == deputy) == deputy).name; // wtf
        foreach (Party party in Party.parties)
        {
            Debug.Log("Generating deputies of " + party.partyName + ".");
            foreach (Person person in party.deputyList)
            {
                GameObject instance = Instantiate(chairPrefab, chairGroup.transform);
                instance.transform.GetChild(0).GetComponentsInChildren<Text>()[0].text = "Name: " + person.firstName + " " + person.lastName;
                instance.transform.GetChild(0).GetComponentsInChildren<Text>()[1].text = "Party: " + party.partyName;
            }
        }
    }
}
