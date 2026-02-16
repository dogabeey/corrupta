using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/// <summary>
/// Splits layout children into pages and exposes navigation helpers.
/// </summary>
[DisallowMultipleComponent]
public class LayoutPager : MonoBehaviour
{
    [SerializeField]  List<GameObject> managedElements = new List<GameObject>();
    [SerializeField] private Transform layoutRoot;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private int elementsPerPage = 5;
    [SerializeField] private TMP_Text pageIndicator;
    [SerializeField] private string pageFormat = "{0}/{1}";
    [SerializeField] private int currentPage;

    [System.Serializable]
    public class PageChangedEvent : UnityEvent<int, int> { }

    [SerializeField] private PageChangedEvent onPageChanged;


    private int PageCount => Mathf.Max(1, Mathf.CeilToInt(managedElements.Count / (float)elementsPerPage));

    private void Start()
    {
        if (!layoutRoot)
        {
            layoutRoot = transform;
        }

    }
    private void OnEnable()
    {
        InitializeElements();
        WireButton(previousButton, GoToPreviousPage);
        WireButton(nextButton, GoToNextPage);
    }

    private void Update()
    {
        UpdateElementVisibility();
    }

    private void InitializeElements()
    {
        managedElements.Clear();
        managedElements.AddRange(layoutRoot.GetComponentsInChildren<AdvisorsPanelUI>(true).ToList().Where(t => t != layoutRoot).Select(t => t.gameObject));
    }

    public void GoToNextPage()
    {
        ShowPage(Mathf.Clamp(currentPage + 1, 0, PageCount - 1));
    }

    public void GoToPreviousPage()
    {
        ShowPage(Mathf.Clamp(currentPage - 1, 0, PageCount - 1));
    }

    public void GoToPage(int pageIndex)
    {
        ShowPage(Mathf.Clamp(pageIndex, 0, PageCount - 1));
    }

    private void ShowPage(int pageIndex)
    {
        currentPage = pageIndex;
        // Show the elements for the current page, hide the rest
        UpdateElementVisibility();

        UpdateButtons();
        UpdateIndicator();
        onPageChanged?.Invoke(currentPage, PageCount);
    }

    private void UpdateElementVisibility()
    {
        for (int i = 0; i < managedElements.Count; i++)
        {
            int pageOfElement = i / elementsPerPage;
            managedElements[i].SetActive(pageOfElement == currentPage);
        }
    }

    private void UpdateButtons()
    {
        if (previousButton)
        {
            previousButton.interactable = currentPage > 0;
        }

        if (nextButton)
        {
            nextButton.interactable = currentPage < PageCount - 1;
        }
    }

    private void UpdateIndicator()
    {
        if (!pageIndicator)
        {
            return;
        }

        if (managedElements.Count == 0)
        {
            pageIndicator.text = "";
            return;
        }

        pageIndicator.text = string.Format(pageFormat, currentPage + 1, PageCount);
    }

    private static void WireButton(Button button, UnityAction action)
    {
        if (!button)
        {
            return;
        }

        button.onClick.RemoveListener(action);
        button.onClick.AddListener(action);
    }
}
