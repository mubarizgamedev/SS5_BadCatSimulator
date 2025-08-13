using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHandInteractUI : MonoBehaviour
{
    public bool showCatHand;
    public GameObject catHandPrefab; // assign the prefab in Inspector
    public Canvas canvas;            // reference to your UI Canvas
    public float destroyDelay = 0.5f;
    bool canInstantitate = true;
    private void Start()
    {
        canInstantitate = true;
    }
    void Update()
    {
        if (!showCatHand) return;
        if (Input.GetMouseButtonDown(0) && canInstantitate)
        {
            Vector2 screenPos = Input.mousePosition;


            GameObject catHand = Instantiate(catHandPrefab, canvas.transform);
            Sfx_Mainmenu.PlayRandomSound(Sfx_Mainmenu.Instance.tapSound);
            canInstantitate = false;

            // Convert screen point to UI canvas position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPos,
                canvas.worldCamera,
                out Vector2 localPos
            );

            catHand.GetComponent<RectTransform>().anchoredPosition = localPos;

            // Destroy after delay
            Destroy(catHand, destroyDelay);
            Invoke(nameof(CanInstTrue), destroyDelay);
        }
    }

    void CanInstTrue()
    {
        canInstantitate = true;
    }
}
