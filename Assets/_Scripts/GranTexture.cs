using UnityEngine.UI;
using UnityEngine;

public class GranTexture : MonoBehaviour
{
    [Header("First Pet")]
    public Material firstGranMaterial;
    public Button[] textureGran1ChangingButtons;
    public Texture defaultTextureGran1;
    public Texture[] gran1Textures;
    [Space(5)]
    [Header("Second Pet")]
    public Material secondGranMaterial;
    public Button[] textureGran2ChangingButtons;
    public Texture defaultTextureGran2;
    public Texture[] gran2Textures;
    [Space(5)]
    [Header("Third Pet")]
    public Material thirdGranMaterial;
    public Button[] textureGran3ChangingButtons;
    public Texture defaultTextureGran3;
    public Texture[] gran3Textures;


    private void Start()
    {
        for (int i = 0; i < textureGran1ChangingButtons.Length; i++)
        {
            int index = i; // Capture the current index
            textureGran1ChangingButtons[i].onClick.AddListener(() => InterstitialAdCall.Instance.StartLoading (()=> ChangeGran1Texture(index)));
        }
        for (int i = 0; i < textureGran2ChangingButtons.Length; i++)
        {
            int index = i; // Capture the current index
            textureGran2ChangingButtons[i].onClick.AddListener(() => InterstitialAdCall.Instance.StartLoading(() => ChangeGran2Texture(index)));
        }
        for (int i = 0; i < textureGran3ChangingButtons.Length; i++)
        {
            int index = i; // Capture the current index
            textureGran3ChangingButtons[i].onClick.AddListener(() => InterstitialAdCall.Instance.StartLoading(() => ChangeGran3Texture(index)));
        }
    }


    private void OnEnable()
    {
        // Set default textures when the script is enabled
        if (firstGranMaterial != null && defaultTextureGran1 != null)
        {
            firstGranMaterial.mainTexture = defaultTextureGran1;
            firstGranMaterial.SetTexture("_EmissionMap", defaultTextureGran1);
            firstGranMaterial.EnableKeyword("_EMISSION");
        }

        if (secondGranMaterial != null && defaultTextureGran2 != null)
        {
            secondGranMaterial.mainTexture = defaultTextureGran2;
            secondGranMaterial.SetTexture("_EmissionMap", defaultTextureGran2);
            secondGranMaterial.EnableKeyword("_EMISSION");
        }

        if (thirdGranMaterial != null && defaultTextureGran3 != null)
        {
            thirdGranMaterial.mainTexture = defaultTextureGran3;
            thirdGranMaterial.SetTexture("_EmissionMap", defaultTextureGran3);
            thirdGranMaterial.EnableKeyword("_EMISSION");
        }
    }

    private void ChangeGran1Texture(int index)
    {
        foreach (var button in textureGran1ChangingButtons)
        {
            GreenSelector greenSelector = button.GetComponent<GreenSelector>();
            if (greenSelector != null)
            {
                greenSelector.ChangeActiveState(false);
            }
        }
        if (index >= 0 && index < gran1Textures.Length)
        {
            firstGranMaterial.mainTexture = gran1Textures[index];

            // Change emission texture too
            firstGranMaterial.SetTexture("_EmissionMap", gran1Textures[index]);

            // If you want the emission to actually be visible, ensure it's enabled
            firstGranMaterial.EnableKeyword("_EMISSION");

            GreenSelector greenSelector = textureGran1ChangingButtons[index].GetComponent<GreenSelector>();
            if (greenSelector != null)
            {
                greenSelector.ChangeActiveState(true);
            }
        }
        else
        {
            Debug.LogWarning("Index out of range for cat textures.");
        }
    }

    private void ChangeGran2Texture(int index)
    {
        foreach (var button in textureGran2ChangingButtons)
        {
            GreenSelector greenSelector = button.GetComponent<GreenSelector>();
            if (greenSelector != null)
            {
                greenSelector.ChangeActiveState(false);
            }
        }
        if (index >= 0 && index < gran2Textures.Length)
        {
            secondGranMaterial.mainTexture = gran2Textures[index];

            // Change emission texture too
            secondGranMaterial.SetTexture("_EmissionMap", gran2Textures[index]);

            // If you want the emission to actually be visible, ensure it's enabled
            secondGranMaterial.EnableKeyword("_EMISSION");
            GreenSelector greenSelector = textureGran2ChangingButtons[index].GetComponent<GreenSelector>();
            if (greenSelector != null)
            {
                greenSelector.ChangeActiveState(true);
            }
        }
        else
        {
            Debug.LogWarning("Index out of range for cat textures.");
        }
    }

    private void ChangeGran3Texture(int index)
    {
        foreach (var button in textureGran3ChangingButtons)
        {
            GreenSelector greenSelector = button.GetComponent<GreenSelector>();
            if (greenSelector != null)
            {
                greenSelector.ChangeActiveState(false);
            }
        }
        if (index >= 0 && index < gran3Textures.Length)
        {
            thirdGranMaterial.mainTexture = gran3Textures[index];

            // Change emission texture too
            thirdGranMaterial.SetTexture("_EmissionMap", gran3Textures[index]);

            // If you want the emission to actually be visible, ensure it's enabled
            thirdGranMaterial.EnableKeyword("_EMISSION");
            GreenSelector greenSelector = textureGran3ChangingButtons[index].GetComponent<GreenSelector>();
            if (greenSelector != null)
            {
                greenSelector.ChangeActiveState(true);
            }
        }
        else
        {
            Debug.LogWarning("Index out of range for cat textures.");
        }
    }
}
