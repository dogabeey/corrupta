using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class UIBehaviour : MonoBehaviour
{
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
