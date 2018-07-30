using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Simulator: MonoBehaviour
{
    public int turn = 0;
    private int nextUpdate = 0;
    [Range(1,4)] public int simSpeed = 1;

    int Year { get { return (int)(turn / 12f); } }
    int Month { get { return (turn % 12) + 1; } }

    public Text yearText;
    public Text monthText;
    
    public void SetSpeed(Slider value)
    {

        simSpeed = (int)value.value;
    }

    public void NextTurn()
    {
        // event affects right before new day.

        // passing of day
        turn++;
        yearText.text = (Year + DateTime.Now.Year).ToString();
        monthText.text = Month.ToString();
        Country.Instance.UpdateAll();
    }
}