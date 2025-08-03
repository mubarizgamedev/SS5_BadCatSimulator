using System;
using UnityEngine;

public class PianoTile : MonoBehaviour, IInteractable
{

    [SerializeField] AudioSource audioSource;

    [Serializable]
    public struct PinaoData
    {
        public string name;
        public AudioClip audioClip;
    }

    public PinaoData[] pinaoData;

    public static event Action OnInteract;

    private int currentIndex = 0;
    public void Interact()
    {
        OnInteract?.Invoke();
        Debug.Log("Interacted with: " + gameObject.name);
        ChangeTune();

    }

    void ChangeTune()
    {
        Debug.Log("Interacted with: " + gameObject.name);

        if (pinaoData != null && pinaoData.Length > 0)
        {
            audioSource.clip = pinaoData[currentIndex].audioClip;
            audioSource.Play();

            // Cycle to the next index
            currentIndex = (currentIndex + 1) % pinaoData.Length;
        }
        else
        {
            Debug.LogWarning("No Remote Data assigned to " + gameObject.name);
        }
    }

    
}
