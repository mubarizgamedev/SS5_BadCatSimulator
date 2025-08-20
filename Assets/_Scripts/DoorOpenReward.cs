
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpenReward : MonoBehaviour
{
    public bool closeOnEveryLevel;
    public GameObject parentDoorLocked;

    public GameObject openDoor;
    public GameObject closedDoor;

    public Button doorOpenButton;

    private void OnEnable()
    {
        if (closeOnEveryLevel)
        {
            return;
        }
        if(PlayerPrefs.GetInt("DoorOpen",0) == 1) // Check if the door is already open
        {
            openDoor.SetActive(true);
            closedDoor.SetActive(false);
            parentDoorLocked.SetActive(false);
        }
        else
        {
            openDoor.SetActive(false);
            closedDoor.SetActive(true);
            parentDoorLocked.SetActive(true);
        }
    }

    private void Start()
    {
        doorOpenButton.onClick.AddListener(ButtonClicked);
        PlayerCollisionEvents.OnLockDoorTrigger += () => doorOpenButton.gameObject.SetActive(true); 
        PlayerCollisionEvents.OnLockDoorTriggerExit += () => doorOpenButton.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        PlayerCollisionEvents.OnLockDoorTrigger -= () => doorOpenButton.gameObject.SetActive(true);
        PlayerCollisionEvents.OnLockDoorTriggerExit -= () => doorOpenButton.gameObject.SetActive(false);
    }

    void ButtonClicked()
    {
        RewardAdCall.Instance.StartLoading(() =>
        {
            openDoor.SetActive(true);
            closedDoor.SetActive(false);
            parentDoorLocked.SetActive(false);
            doorOpenButton.gameObject.SetActive(false); // Hide the button after opening the door
            SFX_Manager.PlaySound(SFX_Manager.Instance.doorOpenSound); // Play door open sound
            PlayerPrefs.SetInt("DoorOpen", 1); // Save the door open state
        });
    }
}
