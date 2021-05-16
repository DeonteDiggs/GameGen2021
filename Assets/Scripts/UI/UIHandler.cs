using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIHandler: MonoBehaviour
{
    [Header("Handle Enabling/Disabling Menus")]
    [SerializeField] private GameObject[] handleMenuActivations;
    [SerializeField] private GameObject gameTitle;
    [SerializeField] private GameObject backButton;
       

    [SerializeField] private int nextScene;

    private bool isOptionsMenu;
    
    [Header("Handle LevelSelection Menu")]
    private bool isLevelSelectionMenu;
    [SerializeField] private Button[] levelButtons;

    public void OnPlayButton() {
        handleMenuActivations[0].SetActive(false);
        gameTitle.SetActive(false);
        handleMenuActivations[2].SetActive(true);
        backButton.SetActive(true);
        isLevelSelectionMenu = true;
        HandLevelButtonInteractions();
    }

    public void OnOptionsButton() {
        handleMenuActivations[0].SetActive(false);
        gameTitle.SetActive(false);
        handleMenuActivations[1].SetActive(true);
        backButton.SetActive(true);
        isOptionsMenu = true;
    }

    public void OnControlsButton() => handleMenuActivations[3].SetActive(true);

    public void DisableControlsMenu() => handleMenuActivations[3].SetActive(false);
    public void OnBackButton() {

        if (isOptionsMenu)
        {
            handleMenuActivations[0].SetActive(true);
            gameTitle.SetActive(true);
            handleMenuActivations[3].SetActive(false);
            handleMenuActivations[1].SetActive(false);
            backButton.SetActive(false);
            isOptionsMenu = false;
            
        }

        else if (isLevelSelectionMenu)
        {
            handleMenuActivations[0].SetActive(true);
            gameTitle.SetActive(true);
            handleMenuActivations[2].SetActive(false);
            backButton.SetActive(false);
            isLevelSelectionMenu = false;
        }
    }

    public void OnQuitButton() => Application.Quit();
    public void ChangeNextScene() => SceneManager.LoadScene(nextScene);

    void HandLevelButtonInteractions() {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > 0)
                levelButtons[i].interactable = false;
            
        }
    }
}
