using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class UIBehaviour : MonoBehaviour
{
    /// <summary>
    /// UpdateEventStrings are the event strings which cause DrawUI() to execute automatically when an event with the same 
    /// string fires in another piece of code.
    /// Example: EventManager.TriggerEvent("EVENT_NAME_HERE");
    /// </summary>
    protected abstract IEnumerable<string> UpdateEventStrings
    {
        get;
    }

    private void OnEnable()
    {
        foreach (string eventString in UpdateEventStrings)
        {
            EventManager.StartListening(eventString, OnEvent);
        }
    }
    private void OnDisable()
    {
        foreach (string eventString in UpdateEventStrings)
        {
            EventManager.StopListening(eventString, OnEvent);
        }
    }

    protected void OnEvent(EventParam e)
    {
        DrawUI();
    }

    public abstract void DrawUI();
}
