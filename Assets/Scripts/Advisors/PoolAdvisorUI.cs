using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;

public class PoolAdvisorUI : UIBehaviour
{
    internal AdvisorBase poolAdvisor;

    [Header("UI References")]
    public TMP_Text advisorNameText;
    public TMP_Text advisorCostText;
    public LayoutGroup advisorBonusContainer;
    public Image advisorPortrait;
    public Image advisorTypeIcon;
    [Header("Prefabs")]
    public TMP_Text advisorBonusTextPrefab;
    public Button advisorAbilityButtonPrefab;

    protected override string UpdateEventString => "ADVISOR_POOL_UPDATED";
    public override void DrawUI()
    {
        advisorNameText.text = poolAdvisor.AdvisorName;
        advisorCostText.text = poolAdvisor.CostMultiplier.ToString() + "<sprite index=0>";
        advisorPortrait.sprite = Addressables.LoadAssetAsync<Sprite>(string.Format(GameConstants.Gfx.Icons.advisor_portraits, poolAdvisor.PortraitIndex)).WaitForCompletion();
        advisorTypeIcon.sprite = poolAdvisor.Type.AdvisorIcon;

        DrawBonusEffects();
    }

    private void DrawBonusEffects()
    {
        if (advisorBonusContainer == null || advisorBonusTextPrefab == null) return;

        for (int i = advisorBonusContainer.transform.childCount - 1; i >= 0; i--)
            Destroy(advisorBonusContainer.transform.GetChild(i).gameObject);

        var effects = poolAdvisor.BonusEffects;
        if (effects == null) return;

        for (int i = 0; i < effects.Count; i++)
        {
            var t = Instantiate(advisorBonusTextPrefab, advisorBonusContainer.transform);
            t.text = effects[i].GetLabel();
        }
    }
}
