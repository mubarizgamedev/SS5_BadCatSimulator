using UnityEngine;
using UnityEngine.UI;

public class LevelSkip : MonoBehaviour
{
    public Button skipButton;

    private void Start()
    {
        skipButton.onClick.AddListener(SkipCurrentLevel);
    }

    void SkipCurrentLevel()
    {
        GameStateManager.Instance.SkipLevel();
    }

}
