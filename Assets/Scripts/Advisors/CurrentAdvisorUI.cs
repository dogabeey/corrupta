using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;

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
        advisorNameText.text = currentAdvisor.AdvisorName;
        advisorPortrait.sprite = Addressables.LoadAssetAsync<Sprite>(GameConstants.Gfx.Icons.advisor_portrait_set[currentAdvisor.PortraitIndex]).WaitForCompletion();
        advisorTypeIcon.sprite = currentAdvisor.Type.AdvisorIcon;

        DrawBonusEffects();
    }

    private void DrawBonusEffects()
    {
        if (advisorBonusContainer == null || advisorBonusTextPrefab == null) return;

        for (int i = advisorBonusContainer.transform.childCount - 1; i >= 0; i--)
            Destroy(advisorBonusContainer.transform.GetChild(i).gameObject);

        var effects = currentAdvisor.BonusEffects;
        if (effects == null) return;

        for (int i = 0; i < effects.Count; i++)
        {
            var t = Instantiate(advisorBonusTextPrefab, advisorBonusContainer.transform);
            t.text = effects[i].GetLabel();
        }
    }
}
