using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Simulator: MonoBehaviour
{
    public int turn = 0; // Each turn is a week. 52 turns is a year.

    public int Year { get { return turn / 52 + 1; } }
    public int Month { get { return (turn % 52) / 4 + 1; } }
    public string MonthText { get { return MonthIntToText(); } }

    public void Start()
    {
        // first few months stats are adjusted.
        for (int i = 0; i < 12; i++)
        {
            NextTurn(false);
        }
    }
    
    private string MonthIntToText()
    {
        return Month switch
        {
            1 => "January",
            2 => "February",
            3 => "March",
            4 => "April",
            5 => "May",
            6 => "June",
            7 => "July",
            8 => "August",
            9 => "September",
            10 => "October",
            11 => "November",
            12 => "December",
            _ => "Unknown",
        };
    }

    public void NextTurn(bool invokeEvents = true)
    {
        // event affects right before new week.
        EventManager.TriggerEvent(GameConstants.GameEvents.TURN_PASSED);

        // passing of a week
        turn++;
        Country.Instance.UpdateAll();
    }

}