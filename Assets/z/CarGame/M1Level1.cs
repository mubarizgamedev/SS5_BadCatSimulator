using UnityEngine;

public class M1Level1 : LevelBase
{
    public override void StartLevel()
    {
        base.StartLevel();
        Debug.Log("Level 1 started!");
    }

    protected override void OnMissionComplete()
    {
        base.OnMissionComplete();
        Debug.Log("Level 1 complete!");
    }
}