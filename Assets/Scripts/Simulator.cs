using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Simulator : MonoBehaviour
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

    public List<int> GetNextGeneralElection()
    {
        int offsetMonths = GameConstants.Instance.defaultGeneralElectionsMonthOffset;
        int periodYears = GameConstants.Instance.defaultGeneralElectionsPeriodYear;

        // Game starts at Year=1, Month=1. First election is at (offsetMonths) after start.
        // If offsetMonths=12, that's Year=2, Month=1.
        int firstElectionAbsMonth = 1 + Math.Max(0, offsetMonths);

        // Convert current date to an absolute month index starting at 1.
        int currentAbsMonth = (Year - 1) * 12 + Month;

        int periodMonths = Math.Max(1, periodYears * 12);

        int nextElectionAbsMonth;
        if (currentAbsMonth <= firstElectionAbsMonth)
        {
            nextElectionAbsMonth = firstElectionAbsMonth;
        }
        else
        {
            int monthsSinceFirst = currentAbsMonth - firstElectionAbsMonth;
            int periodsPassed = (monthsSinceFirst + periodMonths - 1) / periodMonths; // ceil
            nextElectionAbsMonth = firstElectionAbsMonth + periodsPassed * periodMonths;
        }

        int nextYear = ((nextElectionAbsMonth - 1) / 12) + 1;
        int nextMonth = ((nextElectionAbsMonth - 1) % 12) + 1;

        return new List<int> { nextYear, nextMonth };
    }
}