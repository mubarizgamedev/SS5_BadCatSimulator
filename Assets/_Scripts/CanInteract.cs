using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanInteract : MonoBehaviour
{
    public PlayerInteractor playerInteractor;
    public bool playerInZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = true;
            CheckPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = false;
            CheckPlayer();
        }
    }

    void CheckPlayer()
    {
        if (playerInZone)
        {
            playerInteractor.CanInteract = true;
        }
        else
        {
            playerInteractor.CanInteract = false;
        }
    }
}
