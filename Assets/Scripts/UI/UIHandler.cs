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
    [SerializeField] private GameObject gameTitleBackground;
    [SerializeField] private GameObject backButton;

    [Header("Handle Switching Scene Info")]
    [SerializeField] private int freeModeScene;
    Scene sceneControl;

    private bool isOptionsMenu;
    
    [Header("Handle LevelSelection Menu")]
    private bool isLevelSelectionMenu;
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        sceneControl = SceneManager.GetActiveScene();
    }

    public void OnPlayButton() {
        handleMenuActivations[0].SetActive(false);
        gameTitle.SetActive(false);
        gameTitleBackground.SetActive(false);
        handleMenuActivations[2].SetActive(true);
        backButton.SetActive(true);
        isLevelSelectionMenu = true;
        HandLevelButtonInteractions();
    }

    public void OnOptionsButton() {
        handleMenuActivations[0].SetActive(false);
        gameTitle.SetActive(false);
        gameTitleBackground.SetActive(false);
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
            gameTitleBackground.SetActive(true);
            handleMenuActivations[3].SetActive(false);
            handleMenuActivations[1].SetActive(false);
            backButton.SetActive(false);
            isOptionsMenu = false;
            
        }

        else if (isLevelSelectionMenu)
        {
            handleMenuActivations[0].SetActive(true);
            gameTitle.SetActive(true);
            gameTitleBackground.SetActive(true);
            handleMenuActivations[2].SetActive(false);
            backButton.SetActive(false);
            isLevelSelectionMenu = false;
        }
    }

    public void OnQuitButton() => Application.Quit();
    public void ChangeNextScene() => SceneManager.LoadScene(sceneControl.buildIndex + 1);

    void HandLevelButtonInteractions() {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > 0)
                levelButtons[i].interactable = false;
            
        }
    }

    public void OnFreeModeButton() => SceneManager.LoadScene(freeModeScene);
    
}
