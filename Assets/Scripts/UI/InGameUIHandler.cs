using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class InGameUIHandler : MonoBehaviour
{
    [Header("Handle Enabling/Disabling Menus")]
    [SerializeField] private GameObject[] handleMenuActivations;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private int currentScene;
    [SerializeField] private int nextScene;

    [Header("shipCountInfo")]
    private int shipCount = 0;
    [SerializeField] private Text shipCountText;

    [Header("Timer Info")]
    public Text timer;
    [SerializeField] private int startTimeSeconds;
    [SerializeField] private int startTimeMinutes;
    private int startTimeMiliSeconds = 1000;
    private int defaultTime = 0;
    private float currentSeconds;
    public static bool stopTimer;
    public bool isFreeMode;

    private void Start()
    {
       
        
        if (isFreeMode)
        {
            TimeSettings();
        }
        else
            Time.timeScale = 1f;

    }

    private void Update()
    {
        if (!stopTimer)
            Timer();
    }

    void TimeSettings()
    {
        Time.timeScale = 1;
        stopTimer = false;
        defaultTime += ((startTimeSeconds) * startTimeMiliSeconds + (startTimeMinutes * 60) * startTimeMiliSeconds);
        currentSeconds = defaultTime;
    }

    void Timer()
    {
        currentSeconds -= Time.deltaTime * 1000;
        if (currentSeconds <= 0)
        {
            timer.text = "00:00";

            stopTimer = true;

        }
        else
        {
            timer.text = TimeSpan.FromMilliseconds(currentSeconds).ToString(@"mm\:ss");
        }

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

    public void ChangeNextScene() => SceneManager.LoadScene(nextScene);

    public void ChangeShipCount() {

        shipCount++;
        shipCountText.text = "" + shipCount;
    }

    public void ResultMenu()
    {
        handleMenuActivations[3].SetActive(true);

        shipCountText.text = "" + shipCount;

        Time.timeScale = 0;
    }
}
