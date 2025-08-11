using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public Button hintActiveButton;

    private void Start()
    {
        if (!hintActiveButton)
        { 
            Debug.LogError("Hint Button not assigned"); 
            return; 
        }
        hintActiveButton.onClick.AddListener(HintButtonClicked);
    }

    private void HintButtonClicked()
    {
        RewardAdCall.Instance.StartLoading(ActionToDo);
    }

    void ActionToDo()
    {
        NewObjectiveManager.Instance.IndicatorHint();
    }
}
