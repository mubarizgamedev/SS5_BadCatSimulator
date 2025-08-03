
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Update_UI : MonoBehaviour
{
    [SerializeField] Text updateText;
    [SerializeField] GameObject bgToHide;
    string lastString;
    public void ShowTextUpdate(string text, float time)
    {
        lastString = null;
        lastString = text;
        StartCoroutine(EnableDisableCoroutine(text));
    }

    IEnumerator EnableDisableCoroutine(string text)
    {
        bgToHide.SetActive(true);
        updateText.text = text;
        yield return new WaitForSeconds(4f);
        bgToHide.SetActive(false);
    }

    public void UpdateLastOne()
    {
        StartCoroutine(EnableDisableCoroutine(lastString));
    }
}
