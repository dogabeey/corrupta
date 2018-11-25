using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParliamentUI : MonoBehaviour
{
    public GameObject chairPrefab;

    private void Start()
    {
        //nameText = GetComponentsInChildren<Text>()[0];
        //partyText = GetComponentsInChildren<Text>()[1];

        //nameText.text = deputy.firstName + deputy.lastName;
        //partyText.text = Party.parties.Find(pt => pt.deputyList.Find(pr => pr == deputy) == deputy).name; // wtf
        foreach (Party party in Party.parties)
        {
            foreach (Person person in party.deputyList)
            {
                GameObject instance = Instantiate(chairPrefab, chairPrefab.transform.parent);
                instance.GetComponentsInChildren<Text>()[0].text = "Name: " + person.firstName + " " + person.lastName;
                instance.GetComponentsInChildren<Text>()[1].text = "Party: " + party.partyName;
            }
        }
    }
}
