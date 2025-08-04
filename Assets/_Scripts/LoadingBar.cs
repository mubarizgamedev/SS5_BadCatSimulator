using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Text loadingText;
    public string loadingMessage = "Loading...";
    public Image fillImage;
    public float fillDuration = 3f; // Total time in seconds to fill the bar

    private bool startLoading;
    private float elapsedTime;

    private void OnEnable()
    {
        startLoading = true;
        elapsedTime = 0f;
        fillImage.fillAmount = 0f;
        loadingText.text = loadingMessage;
    }

    private void Update()
    {
        if (startLoading)
        {
            if (elapsedTime < fillDuration)
            {
                elapsedTime += Time.deltaTime;
                fillImage.fillAmount = Mathf.Clamp01(elapsedTime / fillDuration);
            }
            else
            {
                fillImage.fillAmount = 1f;
                startLoading = false;
            }
        }
    }
}
