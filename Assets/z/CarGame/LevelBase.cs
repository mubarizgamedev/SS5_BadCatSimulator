using UnityEngine;

public abstract class LevelBase : MonoBehaviour
{
    [Header("Mission Info")]
    public string missionDescription = "Default mission";
    public int totalPointsRequired = 0;

    protected int currentPoints = 0;

    // Called when the level starts
    public virtual void StartLevel()
    {
        Debug.Log($"Mission: {missionDescription}");
        Debug.Log($"Points needed: {totalPointsRequired}");
    }

    // Call this to add points during gameplay
    public virtual void AddPoints(int amount)
    {
        currentPoints += amount;
        Debug.Log($"Points: {currentPoints}/{totalPointsRequired}");

        if (currentPoints >= totalPointsRequired)
        {
            OnMissionComplete();
        }
    }

    // Override this for custom win logic
    protected virtual void OnMissionComplete()
    {
        GameStateEvents.LevelComplete();
    }

    // Optional reset method
    public virtual void ResetLevel()
    {

        currentPoints = 0;
        StartLevel();
    }
}
