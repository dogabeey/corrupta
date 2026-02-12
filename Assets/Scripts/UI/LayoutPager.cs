using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Splits layout children into pages and exposes navigation helpers.
/// </summary>
[DisallowMultipleComponent]
public class LayoutPager : MonoBehaviour
{
    [SerializeField] private Transform layoutRoot;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField, Min(1)] private int elementsPerPage = 6;
    [SerializeField] private bool includeInactiveChildren;
    [SerializeField] private Text pageIndicator;

    [System.Serializable]
    public class PageChangedEvent : UnityEvent<int, int> { }

    [SerializeField] private PageChangedEvent onPageChanged;

    private readonly List<GameObject> managedElements = new List<GameObject>();
    private int currentPage;

    private int PageCount => Mathf.Max(1, Mathf.CeilToInt(managedElements.Count / (float)elementsPerPage));

    private void Awake()
    {
        if (!layoutRoot)
        {
            layoutRoot = transform;
        }

        WireButton(previousButton, GoToPreviousPage);
        WireButton(nextButton, GoToNextPage);
        Refresh();
    }

    private void OnEnable()
    {
        Refresh();
    }

    /// <summary>
    /// Rebuilds the cached element list and reapplies the current page.
    /// </summary>
    public void Refresh()
    {
        CacheElements();
        ShowPage(Mathf.Clamp(currentPage, 0, PageCount - 1));
    }

    private void CacheElements()
    {
        managedElements.Clear();
        if (!layoutRoot)
        {
            return;
        }

        foreach (Transform child in layoutRoot)
        {
            if (!includeInactiveChildren && !child.gameObject.activeSelf)
            {
                continue;
            }
            managedElements.Add(child.gameObject);
        }
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
        if (managedElements.Count == 0)
        {
            currentPage = 0;
            UpdateButtons();
            UpdateIndicator();
            onPageChanged?.Invoke(0, 0);
            return;
        }

        currentPage = pageIndex;
        int startIndex = currentPage * elementsPerPage;
        int endIndex = startIndex + elementsPerPage;
        for (int i = 0; i < managedElements.Count; i++)
        {
            bool shouldBeActive = i >= startIndex && i < endIndex;
            managedElements[i].SetActive(shouldBeActive);
        }

        UpdateButtons();
        UpdateIndicator();
        onPageChanged?.Invoke(currentPage, PageCount);
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
            pageIndicator.text = "0/0";
            return;
        }

        pageIndicator.text = string.Format("{0}/{1}", currentPage + 1, PageCount);
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
