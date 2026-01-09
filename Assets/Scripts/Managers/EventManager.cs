using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventManager", menuName = "Corrupta/Managers/Event Manager...", order = 1)]
public class EventManager : ScriptableObject
{
    private Dictionary<string, Action<EventParam>> eventDictionary;

    public static EventManager Instance => GameManager.Instance.eventManager;

    public void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<EventParam>>();
        }
    }

    public static void StartListening(string eventName, Action<EventParam> listener)
    {
        if(Instance == null) return;

        Action<EventParam> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Add more event to the existing one
            thisEvent += listener;

            //Update the Dictionary
            Instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            //Add event to the Dictionary for the first time
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<EventParam> listener)
    {
        if(Instance == null) return;

        Action<EventParam> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Remove event from the existing one
            thisEvent -= listener;

            //Update the Dictionary
            Instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, EventParam eventParam)
    {
        Action<EventParam> thisEvent = Instance.eventDictionary.GetValueOrDefault(eventName);
        if (thisEvent != null)
        {
            thisEvent.Invoke(eventParam);
            // OR USE  instance.eventDictionary[eventName](eventParam);
        }
    }
}

public class EventParam
{
    public GameObject paramObj;
    public int paramInt;
    public string paramStr;
    public Type paramType;
    public Dictionary<string, object> paramDictionary;
    public bool paramBool;
    public float paramFloat;
    public Vector3 paramVec3;

    public EventParam()
    {

    }

    public EventParam(Dictionary<string, object> paramDictionary)
    {
        this.paramDictionary = paramDictionary;
    }

    public EventParam(GameObject paramObj = null, int paramInt = 0, string paramStr = "", Type paramType = null, Dictionary<string, object> paramDictionary = null,
        bool paramBool = false, float paramFloat = 0f, Vector3 paramVec3 = new Vector3())
    {
        this.paramObj = paramObj;
        this.paramInt = paramInt;
        this.paramStr = paramStr;
        this.paramType = paramType;
        this.paramDictionary = paramDictionary;
        this.paramBool = paramBool;
        this.paramFloat = paramFloat;
        this.paramVec3 = paramVec3;
    }
}
