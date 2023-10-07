using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public GameObject contentPanel;

    public void LoadScene(string scene)
    {
        LoadScene(scene);
    }

    public void DeactivateContent()
    {
        for (int i = 0; i < contentPanel.transform.childCount; i++)
        {
            contentPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
