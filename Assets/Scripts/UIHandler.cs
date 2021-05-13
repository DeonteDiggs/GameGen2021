using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler: MonoBehaviour
{
    
    [SerializeField] private GameObject[] enableMenus;
    [SerializeField] private GameObject[] disableMenus;
    [SerializeField] private GameObject backButton;

    private bool isOptionsMenu;
    private bool isLevelSelectionMenu;

    public void OnPlayButton() {
        enableMenus[0].SetActive(false);
        enableMenus[2].SetActive(true);
        backButton.SetActive(true);
        isLevelSelectionMenu = true;
    }

    public void OnOptionsButton() {
        enableMenus[0].SetActive(false);
        enableMenus[1].SetActive(true);
        backButton.SetActive(true);
        isOptionsMenu = true;
    }

    public void OnBackButton() {

        if (isOptionsMenu)
        {
            enableMenus[0].SetActive(true);
            enableMenus[1].SetActive(false);
            backButton.SetActive(false);
            isOptionsMenu = false;
        }

        else if (isLevelSelectionMenu)
        {
            enableMenus[0].SetActive(true);
            enableMenus[2].SetActive(false);
            backButton.SetActive(false);
            isLevelSelectionMenu = false;
        }
    }

    public void OnQuitButton() {
        Application.Quit();
    }
}
