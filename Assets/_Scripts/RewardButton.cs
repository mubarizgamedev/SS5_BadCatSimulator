using UnityEngine;

public class RewardButton : MonoBehaviour
{

    public GameObject currentRewardedGameobject;
    [SerializeField] PlayerRaycaster playerRaycaster;
    [SerializeField] AdAfter40Sec AdAfter40Sec;



    private void Start()
    {
        playerRaycaster.OnInteractedWithRewarded += PlayerRaycaster_OnInteractedWithRewarded;
    }

    private void PlayerRaycaster_OnInteractedWithRewarded(object sender, PlayerRaycaster.OnInteractedWithRewardedClass e)
    {
        currentRewardedGameobject = e.rewardedGameObject;
    }

    public void Btn_Reward()
    {
        ShowReward();
    }
    
    void ActionReward()
    {
        currentRewardedGameobject.layer = 8;
        AdAfter40Sec.ResetAdTimer();
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_Reward_Done("REWARD GRANTED");
        }
    }

    void ShowReward()
    {
        RewardAdCall.Instance.StartLoading(ActionReward);
    }
}