using UnityEngine;
using UnityEngine.UI;

public class InappsCross : MonoBehaviour
{
    public AdAfter40Sec adAfter40Sec;

    [Header("Panel 1")]
    public Button btnInappCross;
    public GameObject inappPanel;
    [Header("Panel 2")]
    public Button btnInappCross2;
    public GameObject inappPanel2;

    public SpecialAttack_PopUp SpecialAttack_PopUp;
    public GameObject[] specialAttacksPanels;

    [Space(5)]
    [Header("Special Attacks Cross")]
    public Button[] specialAttackCrosButtons;


    private void Start()
    {
        btnInappCross.onClick.AddListener(OnInappCrossButtonClicked);
        btnInappCross2.onClick.AddListener(OnInappCrossButtonClicked);

        foreach (Button button in specialAttackCrosButtons)
        {
            button.onClick.AddListener(SpecialAttackCrossPressed);
        }
    }

    void OnInappCrossButtonClicked()
    {
        SFX_Manager.PlaySound(SFX_Manager.Instance.ButtonClick);
        InterstitialAdCall.Instance.StartLoading(Work);
    }

    void Work()
    {
        adAfter40Sec.ResetAdTimer();
        inappPanel.SetActive(false);
        inappPanel2.SetActive(false);
        DisableAllPanels();
    }

    void DisableAllPanels()
    {
        foreach (GameObject panel in specialAttacksPanels)
        {
            panel.SetActive(false);

        }
    }

    void SpecialAttackCrossPressed()
    {
        DisableAllPanels();
        SpecialAttack_PopUp.ResetSpeacialTimer();
    }
}
