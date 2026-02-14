using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentAdvisorUI : UIBehaviour
{
    protected AdvisorBase currentAdvisor;

    [Header("UI References")]
    public TMP_Text advisorNameText;
    public LayoutGroup advisorBonusContainer;
    public LayoutGroup advisorAbilityContainer;
    public Image advisorPortrait;
    public Image advisorTypeIcon;
    [Header("Prefabs")]
    public TMP_Text advisorBonusTextPrefab;
    public Button advisorAbilityButtonPrefab;

    protected override string UpdateEventString => "ADVISOR_UPDATED";

    public override void DrawUI()
    {
    }
}
