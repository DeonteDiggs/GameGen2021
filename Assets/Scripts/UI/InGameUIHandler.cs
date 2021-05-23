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

    [Header("Handle Switching Scene Info")]
    [SerializeField] private int GameOverScene;
    Scene sceneControl;
    public bool isInGameScene;

    [Header("Ship Count Info")]
    public static int shipCount = 0;
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
            SetCount();
        }

        else if(isInGameScene)
        {

            Time.timeScale = 1f;
            sceneControl = SceneManager.GetActiveScene();
           
            SetCount();
        }
            

    }

    private void Update()
    {
        if (!stopTimer && isFreeMode)
            Timer();
    }

    void SetCount() {

        shipCount = 3;
        GameSaveManager.gameSaveManager.saveData.trackBoatCount = shipCount;
        
        shipCountText.text = "" + shipCount;
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
            StartCoroutine(WaitAfterTimerIsZero());
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
    public void OnRestartButton() => ChangeScene(sceneControl.buildIndex);
    public void OnContinueButton() => ChangeScene(sceneControl.buildIndex + 1);
    public void OnTryAgainButton() => ChangeScene(GameSaveManager.gameSaveManager.saveData.trackSceneIndex);


    public void OnResumeButton() {
        handleMenuActivations[0].SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OnQuitButton(int changeScene) => ChangeScene(changeScene);

    public void OnControlsButton() => handleMenuActivations[2].SetActive(true);

    public void OnBackButton() {
        handleMenuActivations[1].SetActive(false);
        handleMenuActivations[0].SetActive(true);
    }

    public void ChangeScene(int changeScene) => SceneManager.LoadScene(changeScene);

    public void ChangeShipCount() {

        if (shipCount > 0)
        {
            shipCount--;
            GameSaveManager.gameSaveManager.saveData.trackBoatCount = shipCount;
            GameSaveManager.gameSaveManager.SaveGame();
            shipCountText.text = "" + shipCount;
        }
        
        if (shipCount <= 0)
        {
            
            GameSaveManager.gameSaveManager.saveData.trackSceneIndex = sceneControl.buildIndex;
            GameSaveManager.gameSaveManager.SaveGame();
            GameOver();
        }
    }

    public void ResultMenu()
    {
        handleMenuActivations[3].SetActive(true);
        //GameSaveManager.gameSaveManager.LoadGame();
        
        int num = GameSaveManager.gameSaveManager.saveData.trackBoatCount;
      
        shipCountText.text = num.ToString();
        print(shipCountText.text);
        
    }

    public void GameOver() => SceneManager.LoadScene(GameOverScene);

    public IEnumerator WaitAfterTimerIsZero()
    {

        yield return new WaitForSeconds(1.5f);
        ResultMenu();
    }

}
