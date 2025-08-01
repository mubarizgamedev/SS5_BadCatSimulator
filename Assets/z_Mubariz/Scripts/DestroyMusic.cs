using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMusic : MonoBehaviour
{
    public void DestroyFirstMusic()
    {
        // Find the first object in the scene with the FirstMusic script
        FirstMusic music = FindObjectOfType<FirstMusic>();

        if (music != null)
        {
            Destroy(music.gameObject); // Destroys the entire GameObject
            Debug.Log("FirstMusic GameObject destroyed.");
        }
        else
        {
            Debug.LogWarning("FirstMusic script not found in the scene.");
        }
    }
}
