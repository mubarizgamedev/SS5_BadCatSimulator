using UnityEngine;
using UnityEngine.UI;

public class GiftDiamond : MonoBehaviour
{
    public Button diamondGiftButton;
    public GameObject diamondVisualsUp;
    public int coins = 10; // Number of coins to add when the button is clicked

    private void Start()
    {
        diamondGiftButton.onClick.AddListener(()=> RewardAdCall.Instance.StartLoading(()=> OnDiamondGiftButtonClicked()));
    }

    void OnDiamondGiftButtonClicked()
    {
        diamondVisualsUp.SetActive(true);
        int prevCoins = PlayerPrefs.GetInt("MyCoins", 0);
        prevCoins += coins;
        Debug.Log("Coins = " + prevCoins);
        PlayerPrefs.SetInt("MyCoins", prevCoins);
    }
}
