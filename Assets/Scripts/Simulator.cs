﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Simulator: MonoBehaviour
{
    public int turn = 0;
    [Range(1,4)] public int simSpeed = 1;

    public int Year { get { return (int)(turn / 12f); } }
    public int Month { get { return (turn % 12) + 1; } }

    public Text yearText;
    public Text monthText;
    public GameObject gameCanvas;

    public void Start()
    {
        // first few months stats are adjusted.
        for (int i = 0; i < 12; i++)
        {
            NextTurn(false);
        }
    }
    
    public void SetSpeed(Slider value)
    {

        simSpeed = (int)value.value;
    }

    public void NextTurn(bool invokeEvents = true)
    {
        // event affects right before new day.

        // passing of day
        turn++;
        yearText.text = (Year + DateTime.Now.Year).ToString();
        monthText.text = Month.ToString();

        Country.Instance.UpdateAll();
    }

}