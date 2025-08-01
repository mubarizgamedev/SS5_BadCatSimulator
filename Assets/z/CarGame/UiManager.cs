using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [Header("Panels")]
    public GameObject levelCompletePanel;
    public GameObject levelFailPanel;
    public GameObject gamePausePanel;
    public GameObject gameSettingPanel;

    [Header("Buttons")]
    public Button nextLevelButton;
    public Button homeButton;
    public Button restartButton;
    public Button settingButton;
    public Button settingBackButton;
    public Button pauseButton;

    [Header("Others")]
    public LevelManager levelManager;
    public string mainMenuSceneName;

    private void Start()
    {
        GameStateEvents.OnLevelComplete += LevelComplete;
        GameStateEvents.OnLevelFail += LevelFail;
        GameStateEvents.OnGamePaused += GamePause;
        GameStateEvents.OnGameResumed += GameResume;
        GameStateEvents.OnSettingOpen += GameSetting;



        //BUTTONS ASSIGNMENTS
        nextLevelButton.onClick.AddListener(NextLevel);
        homeButton.onClick.AddListener(Home);
        restartButton.onClick.AddListener(Restart);
        settingButton.onClick.AddListener(GameSetting);
        settingBackButton.onClick.AddListener(GameSettingBack);
        pauseButton.onClick.AddListener(GamePause);
    }

    private void OnDestroy()
    {
        GameStateEvents.OnLevelComplete -= LevelComplete;
        GameStateEvents.OnLevelFail -= LevelFail;
        GameStateEvents.OnGamePaused -= GamePause;
        GameStateEvents.OnGameResumed -= GameResume;
        GameStateEvents.OnSettingOpen -= GameSetting;
    }

    void LevelComplete()
    {
        levelCompletePanel.SetActive(true);
    }
    void LevelFail()
    {
        Time.timeScale = 0;
        levelFailPanel.SetActive(true);
    }
    void GamePause()
    {
        gamePausePanel.SetActive(true);
        TimeScaleZero();
    }
    void GameResume()
    {
        gamePausePanel.SetActive(false);
        TimeScaleOne();
    }
    void GameSetting()
    {
        gameSettingPanel.SetActive(true);
        TimeScaleZero();
    }
    void GameSettingBack()
    {
        gameSettingPanel.SetActive(false);
        TimeScaleOne();
    }
    void NextLevel()
    {
        levelManager.NextLevel();
        TimeScaleOne();
    }

    void Restart()
    {
        levelManager.RestartLevel();
        levelFailPanel.SetActive(false);
        gamePausePanel.SetActive(false);
        TimeScaleOne();
    }

    void Home()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }





    // Helper methods to control time scale
    void TimeScaleZero()
    {
        Time.timeScale = 0f;
    }
    void TimeScaleOne()
    {
        Time.timeScale = 1f;
    }
}
