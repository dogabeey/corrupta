using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class UIBehaviour : MonoBehaviour
{
    /// <summary>
    /// UpdateEventString is the event string which causes DrawUI() to execute automatically when an event with the same 
    /// string fires in another piece of code.
    /// Example: EventManager.TriggerEvent("EVENT_NAME_HERE");
    /// </summary>
    protected abstract string UpdateEventString
    {
        get;
    }

    private void OnEnable()
    {
        EventManager.StartListening(UpdateEventString, OnEvent);
    }
    private void OnDisable()
    {
        EventManager.StopListening(UpdateEventString, OnEvent);
    }

    protected void OnEvent(EventParam e)
    {
        DrawUI();
    }

    public abstract void DrawUI();
}
