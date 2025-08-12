using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatTextures : MonoBehaviour
{
    [Header("First Pet")]
    public Material firstPetMaterial;
    public Button[] texturePet1ChangingButtons;
    public Texture[] pet1Textures;
    [Space(5)]
    [Header("Second Pet")]
    public Material secondPetMaterial;
    public Button[] texturePet2ChangingButtons;
    public Texture[] pet2Textures;
    [Space(5)]
    [Header("Third Pet")]
    public Material thirdPetMaterial;
    public Button[] texturePet3ChangingButtons;
    public Texture[] pet3Textures;


    private void Start()
    {
        for (int i = 0; i < texturePet1ChangingButtons.Length; i++)
        {
            int index = i; // Capture the current index
            texturePet1ChangingButtons[i].onClick.AddListener(() => ChangePet1Texture(index));
        }
        for (int i = 0; i < texturePet2ChangingButtons.Length; i++)
        {
            int index = i; // Capture the current index
            texturePet2ChangingButtons[i].onClick.AddListener(() => ChangePet2Texture(index));
        }
        for (int i = 0; i < texturePet3ChangingButtons.Length; i++)
        {
            int index = i; // Capture the current index
            texturePet3ChangingButtons[i].onClick.AddListener(() => ChangePet3Texture(index));
        }


    }

    private void ChangePet1Texture(int index)
    {
        if (index >= 0 && index < pet1Textures.Length)
        {
            firstPetMaterial.mainTexture = pet1Textures[index];

            // Change emission texture too
            firstPetMaterial.SetTexture("_EmissionMap", pet1Textures[index]);

            // If you want the emission to actually be visible, ensure it's enabled
            firstPetMaterial.EnableKeyword("_EMISSION");
        }
        else
        {
            Debug.LogWarning("Index out of range for cat textures.");
        }
    }

    private void ChangePet2Texture(int index)
    {
        if (index >= 0 && index < pet2Textures.Length)
        {
            secondPetMaterial.mainTexture = pet2Textures[index];

            // Change emission texture too
            secondPetMaterial.SetTexture("_EmissionMap", pet2Textures[index]);

            // If you want the emission to actually be visible, ensure it's enabled
            secondPetMaterial.EnableKeyword("_EMISSION");
        }
        else
        {
            Debug.LogWarning("Index out of range for cat textures.");
        }
    }

    private void ChangePet3Texture(int index)
    {
        if (index >= 0 && index < pet3Textures.Length)
        {
            thirdPetMaterial.mainTexture = pet3Textures[index];

            // Change emission texture too
            thirdPetMaterial.SetTexture("_EmissionMap", pet3Textures[index]);

            // If you want the emission to actually be visible, ensure it's enabled
            thirdPetMaterial.EnableKeyword("_EMISSION");
        }
        else
        {
            Debug.LogWarning("Index out of range for cat textures.");
        }
    }

}
