using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MouleMiniGameHandler : MonoBehaviour
{
    public Button playGameButton;
    public Button backButton;
    public Button gameBackButton;
    public GameObject playMiniGamePanel;
    public GameObject fadePanel;
    public GameObject gameplayUi;
    public GameObject miniGameUi;
    public GameObject cameraMiniGame;
    public AdAfter40Sec AdAfter40Sec;
    public SpecialAttack_PopUp specialAttack_PopUp;


    private void Start()
    {
        playGameButton.onClick.AddListener(OnPlayGameButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
        gameBackButton.onClick.AddListener(OnBackButtonClicked);
        playMiniGamePanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playMiniGamePanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playMiniGamePanel.SetActive(false);
        }
    }

    private void OnPlayGameButtonClicked()
    {
        Debug.Log("Starting Mini-Game...");
        EnemyHandler.Instance.ResetState();
        StartCoroutine(StartGameCoroutine());
        
    }

    IEnumerator StartGameCoroutine()
    {
        AdAfter40Sec.Instance.ResetAdTimer();
        SpecialAttack_PopUp.Instance.ResetSpeacialTimer();
        AdAfter40Sec.gameObject.SetActive(false);
        specialAttack_PopUp.gameObject.SetActive(false);

        playMiniGamePanel.SetActive(false);
        fadePanel.SetActive(true);
        yield return new WaitForSeconds(1.5f); // Wait for fade effect
        gameplayUi.SetActive(false);
        miniGameUi.SetActive(true);
        cameraMiniGame.SetActive(true);
        yield return new WaitForSeconds(1.5f); // Wait for fade effect
        fadePanel.SetActive(false);
    }

    private void OnBackButtonClicked()
    {
        Debug.Log("Going back to the main menu...");
        InterstitialAdCall.Instance.StartLoading(() => playMiniGamePanel.SetActive(false));
        StartCoroutine(BackCoroutine());
    }
    IEnumerator BackCoroutine()
    {
        AdAfter40Sec.gameObject.SetActive(true);
        specialAttack_PopUp.gameObject.SetActive(true);
        AdAfter40Sec.Instance.ResetAdTimer();
        SpecialAttack_PopUp.Instance.ResetSpeacialTimer();

        fadePanel.SetActive(true);
        yield return new WaitForSeconds(1.5f); // Wait for fade effect
        gameplayUi.SetActive(true);
        miniGameUi.SetActive(false);
        cameraMiniGame.SetActive(false);
        yield return new WaitForSeconds(1.5f); // Wait for fade effect
        fadePanel.SetActive(false);
    }
}