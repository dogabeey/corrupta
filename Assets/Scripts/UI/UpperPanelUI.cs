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
    public Image remainingAPFill;

    protected override string UpdateEventString => GameConstants.GameEvents.TURN_PASSED;

    public override void DrawUI()
    {
    }
}
