using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Simulator: MonoBehaviour
{
    public int turn = 0;

    int Year { get { return (int)(turn / 12f); } }
    int Month { get { return (turn % 12) + 1; } }

    public Text yearText;
    public Text monthText;

    public void NextTurn()
    {
        // event affects right before new day.

        // passing of day
        turn++;
        yearText.text = Year.ToString();
        monthText.text = Month.ToString();
        Country.Instance.UpdateAll();
    }
}