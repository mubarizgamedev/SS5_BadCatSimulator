using UnityEngine;
using System;

public class Progress : MonoBehaviour
{
    public GameObject nextGameObject;    // The next GameObject to activate
    public bool lastCheckPoint = false;  // Is this the final checkpoint?

    public static event Action OnLevelComplete;  // Event triggered on final checkpoint

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (nextGameObject != null)
                nextGameObject.SetActive(true);

            if (lastCheckPoint)
            {
                GameStateEvents.LevelComplete();
            }
        }
    }
}
