using UnityEngine;
using System;

public class SpecialManager : MonoBehaviour
{
    [Header("Boxing")]
    [SerializeField] GameObject fireRewardPanel;
    [SerializeField] GameObject beeRewardPanel;
    [SerializeField] GameObject currentRewardPanel;
    [SerializeField] GameObject boxingRewardPanel;
    [Space(10)]
    [SerializeField] GameObject playerBoxingGlove;
    [SerializeField] GameObject playerShockGun;
    [Header("UI Button")]
    [SerializeField] GameObject boxingUI;
    [SerializeField] GameObject ShockGunUI;
    [SerializeField] GameObject fireButtonUI;
    [SerializeField] GameObject beeButtonUI;
    [SerializeField] AdAfter40Sec AdAfter40Sec;
    [SerializeField] GameObject rewardLoadingPanel;



    private void Start()
    {
        PlayerCollisionEvents.OnPuchBoxTrigger += OnPunchButtonPressed;
        PlayerCollisionEvents.OnShockGunTrigger += OnShockButtonPressed;
    }
    private void OnDestroy()
    {
        PlayerCollisionEvents.OnPuchBoxTrigger -= OnPunchButtonPressed;
        PlayerCollisionEvents.OnShockGunTrigger -= OnShockButtonPressed;
    }



    public void OnShockButtonPressed()
    {
        currentRewardPanel.SetActive(true);
    }

    public void OnPunchButtonPressed()
    {
        boxingRewardPanel.SetActive(true);
    }

    public void OnFireButtonPressed()
    {
        fireRewardPanel.SetActive(true);
    }
    public void OnBeeButtonPressed()
    {
        beeRewardPanel.SetActive(true);
    }




    public void RewardButtonBoxing()
    {
        ShowRewardForPunch();
    }
    public void RewardButtonShockGun()
    {
        ShowRewardForShock();
    }
    public void RewardButtonBeeAttack()
    {
        ShowRewardForBee();
    }
    public void RewardButtonFireAttack()
    {
        ShowRewardForFire();
    }


    void ShowRewardForPunch()
    {
        RewardAdCall.Instance.StartLoading(() =>
        {
            ActionAfterBoxingReward();
        });
    }

    void ActionAfterBoxingReward()
    {
        boxingRewardPanel.SetActive(false);
        playerBoxingGlove.SetActive(true);
        SpecialItemInHand.Instance.SetItemState(playerBoxingGlove, true);
        boxingUI.SetActive(true);
        AdAfter40Sec.ResetAdTimer();
    }

    void ShowRewardForShock()
    {
        RewardAdCall.Instance.StartLoading(() =>
        {
            ActionAfterShockReward();
        });
    }
    void ActionAfterShockReward()
    {
        currentRewardPanel.SetActive(false);
        ShockGunUI.SetActive(true);
        SpecialItemInHand.Instance.SetItemState(playerShockGun, true);
        playerShockGun.SetActive(true);
        AdAfter40Sec.ResetAdTimer();
    }

    void ShowRewardForFire()
    {
        RewardAdCall.Instance.StartLoading(() =>
        {
            ActionAfterFireReward();
        });
    }
    void ActionAfterFireReward()
    {
        fireRewardPanel.SetActive(false);
        fireButtonUI.SetActive(true);
        playerShockGun.SetActive(true);
        SpecialItemInHand.Instance.SetItemState(playerShockGun, true);
        AdAfter40Sec.ResetAdTimer();
    }



    void ShowRewardForBee()
    {
        RewardAdCall.Instance.StartLoading(() =>
        {
            ActionAfterBeeReward();
        });
    }
    void ActionAfterBeeReward()
    {
        beeRewardPanel.SetActive(false);
        beeButtonUI.SetActive(true);
        playerShockGun.SetActive(true);
        SpecialItemInHand.Instance.SetItemState(playerShockGun, true);
        AdAfter40Sec.ResetAdTimer();
    }

    public void DisableAllPanels()
    {
        boxingRewardPanel.SetActive(false);
        currentRewardPanel.SetActive(false);
        fireRewardPanel.SetActive(false);
        beeRewardPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BoxUIButtonPressed()
    {
        SpecialItemInHand.Instance.SetItemState(playerBoxingGlove, false);
    }

    public void GuncUIButtonPressed()
    {
        SpecialItemInHand.Instance.SetItemState(playerShockGun, false);
    }

   
    
}
