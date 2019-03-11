using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ReportUI : MonoBehaviour
{
    public GameObject eventLog;
    public Toggle eventNameTextPrefab;
    public Text eventDescriptionText;

    //public GameObject general, people, government, team, party, parliament, city, country; // Whenever a button is added, add it here. TODO: find more autonomious way to do that.
    void OnEnable()
    {
        eventNameTextPrefab.gameObject.SetActive(true);
        for (int i = 0; i < eventLog.transform.childCount; i++)
        {
            Destroy(eventLog.transform.GetChild(i).gameObject);
        }
        foreach (Event e in Event.invokedEvents)
        {
            GameObject instance = Instantiate(eventNameTextPrefab.gameObject, eventLog.transform);
            instance.GetComponentInChildren<Text>().text = e.header;
            if (e.isRead) instance.GetComponentInChildren<Text>().color = Color.gray;
        }
        eventNameTextPrefab.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void GetDescription()
    {
        GameObject activeToggle = eventLog.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault().gameObject;
        string activeHeader = activeToggle.GetComponentInChildren<Text>().text.ToString();
        Event activeEvent = Event.invokedEvents.Find(e => e.header == activeHeader);
        eventDescriptionText.text = activeEvent.description;
        foreach (Effect e in activeEvent.effects)
        {
            eventDescriptionText.text += "\n\n" + e.ToString();
        }
        Event.gameEvents.Find(e => e.header == activeHeader).isRead = true;
        FindObjectOfType<Simulator>().gameCanvas.SetActive(false);
        FindObjectOfType<Simulator>().gameCanvas.SetActive(true);
    }
}
