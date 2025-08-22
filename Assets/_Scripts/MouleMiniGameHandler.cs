using UnityEngine;
using UnityEngine.UI;

public class MouleMiniGameHandler : MonoBehaviour
{
    public Button playGameButton;
    public Button backButton;
    public GameObject playMiniGamePanel;

    private void Start()
    {
        playGameButton.onClick.AddListener(OnPlayGameButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
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
    }

    private void OnBackButtonClicked()
    {
        Debug.Log("Going back to the main menu...");
        InterstitialAdCall.Instance.StartLoading(() => playMiniGamePanel.SetActive(false));
    }
}