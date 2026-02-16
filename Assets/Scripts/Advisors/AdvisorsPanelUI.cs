using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Container/mediator UI that owns advisor-related UI widgets.
/// Expected usage:
/// - Hook up the lists in the Inspector (or place as children under the given roots)
/// - Call `SetCurrentAdvisor` / `SetPoolAdvisor` to update and redraw.
/// </summary>
public class AdvisorsPanelUI : UIBehaviour
{
    [Header("Events")]
    [SerializeField] private string updateEventString = "ADVISORS_PANEL_UPDATED";

    protected override string UpdateEventString => updateEventString;

    [Header("UI Sections")]
    public Toggle currentAdvisorsButton;
    public Toggle poolAdvisorsButton;
    public LayoutGroup advisorsPoolLayoutGroup;
    public LayoutGroup currentAdvisorsLayoutGroup;
    [Header("Prefabs")]
    public PoolAdvisorUI poolAdvisorUIPrefab;
    public CurrentAdvisorUI currentAdvisorUIPrefab;

    private void Awake()
    {
        currentAdvisorsButton.onValueChanged.AddListener((isOn) =>
        {
            CurrentAdvisorButton_OnClick();
        });
        poolAdvisorsButton.onValueChanged.AddListener((isOn) =>
        {
            PoolAdvisorsButton_OnClick();
        });
    }

    public void CurrentAdvisorButton_OnClick()
    {
        DrawUI();
    }
    public void PoolAdvisorsButton_OnClick()
    {
        DrawUI();
    }

    public override void DrawUI()
    {
        // Clear existing UI elements
        foreach (Transform child in advisorsPoolLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in currentAdvisorsLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        // Populate pool advisors
        foreach (AdvisorBase poolAdvisor in GameManager.Instance.advisorPool.advisors)
        {
            PoolAdvisorUI poolAdvisorUI = Instantiate(poolAdvisorUIPrefab, advisorsPoolLayoutGroup.transform);
            poolAdvisorUI.poolAdvisor = poolAdvisor;
            poolAdvisorUI.DrawUI();
        }

        // Populate current advisors (for the current player person)
        var playerPerson = (GameManager.Instance.players != null && GameManager.Instance.players.Count > 0)
            ? GameManager.Instance.players[0].playerPerson
            : null;
        var playerPersonController = GameManager.Instance.GetPersonController(playerPerson);
        var currentAdvisors = playerPersonController != null ? playerPersonController.Advisors : null;
        if (currentAdvisors != null)
        {
            for (int i = 0; i < currentAdvisors.Count; i++)
            {
                var currentAdvisor = currentAdvisors[i];
                if (currentAdvisor == null) continue;

                CurrentAdvisorUI currentAdvisorUI = Instantiate(currentAdvisorUIPrefab, currentAdvisorsLayoutGroup.transform);
                currentAdvisorUI.currentAdvisor = currentAdvisor;
                currentAdvisorUI.DrawUI();
            }
        }
    }
}
