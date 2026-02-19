using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The panel that contains turn information like current date, turn and next turn button etc.
/// </summary>
public class UpperPanelUI : UIBehaviour
{
    public Button nextTurnButton;
    public TMP_Text currentTurnText;
    public TMP_Text currentDateText;
    public TMP_Text nextElectionText;
    public Image remainingAPFill;

    protected override IEnumerable<string> UpdateEventStrings => new[] { GameConstants.GameEvents.TURN_PASSED };

    public override void DrawUI()
    {
        nextTurnButton.onClick.RemoveAllListeners();
        nextTurnButton.onClick.AddListener(NextTurnButton_OnClick);

        currentTurnText.text = $"Week {GameManager.Instance.simulatorManager.turn}";
        nextElectionText.text = $"Next Election: {GetRemainingTimeToElection()}";
        remainingAPFill.fillAmount = GameManager.Instance.players[0].remainingAP;
    }

    private void NextTurnButton_OnClick()
    {
        GameManager.Instance.simulatorManager.NextTurn();
    }
    private string GetRemainingTimeToElection()
    {
        var simulator = GameManager.Instance.simulatorManager;
        if (simulator == null)
        {
            return string.Empty;
        }

        List<int> next = simulator.GetNextGeneralElection();
        if (next == null || next.Count < 2)
        {
            return string.Empty;
        }

        int nextYear = next[0];
        int nextMonth = next[1];

        int currentAbsMonth = (simulator.Year - 1) * 12 + simulator.Month;
        int nextAbsMonth = (nextYear - 1) * 12 + nextMonth;

        int remainingMonths = Mathf.Max(0, nextAbsMonth - currentAbsMonth);
        int years = remainingMonths / 12;
        int months = remainingMonths % 12;

        if (years > 0 && months > 0)
        {
            return $"{years} year{(years > 1 ? "s" : "")} {months} month{(months > 1 ? "s" : "")}";
        }
        if (years > 0)
        {
            return $"{years} year{(years > 1 ? "s" : "")}";
        }
        return $"{months} month{(months > 1 ? "s" : "")}";
    }
}
