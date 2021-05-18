using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InGameUIHandler : MonoBehaviour
{
    [Header("Handle Enabling/Disabling Menus")]
    [SerializeField] private GameObject[] handleMenuActivations;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private int currentScene;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void OnPauseButton() {
        handleMenuActivations[0].SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void OnOptionsButton() {
        handleMenuActivations[0].SetActive(false);
        handleMenuActivations[1].SetActive(true);
        backButton.SetActive(true);
    }
    public void OnRestartButton() {
        SceneManager.LoadScene(currentScene);
    }
    public void OnResumeButton() {
        handleMenuActivations[0].SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OnQuitButton() => SceneManager.LoadScene(0);

    public void OnControlsButton() => handleMenuActivations[2].SetActive(true);

    public void OnBackButton() {
        handleMenuActivations[1].SetActive(false);
        handleMenuActivations[0].SetActive(true);
    }

}
