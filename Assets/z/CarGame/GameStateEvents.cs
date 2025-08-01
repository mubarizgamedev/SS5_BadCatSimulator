using UnityEngine;
using System;

public class GameStateEvents : MonoBehaviour
{
    // Events
    public static event Action OnLevelComplete;
    public static event Action OnLevelFail;
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;
    public static event Action OnSettingOpen;

    // Singleton (optional, remove if not needed)
    public static GameStateEvents Instance { get; private set; }

    private void Awake()
    {
        // Optional singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persist across scenes
    }

    // Public static methods to trigger events from anywhere
    public static void LevelComplete()
    {
        Debug.Log("Level Complete Triggered");
        OnLevelComplete?.Invoke();
    }

    public static void LevelFail()
    {
        Debug.Log("Level Fail Triggered");
        OnLevelFail?.Invoke();
    }

    public static void GamePaused()
    {
        Debug.Log("Game Paused Triggered");
        OnGamePaused?.Invoke();
    }

    public static void GameResumed()
    {
        Debug.Log("Game Resumed Triggered");
        OnGameResumed?.Invoke();
    }
}
