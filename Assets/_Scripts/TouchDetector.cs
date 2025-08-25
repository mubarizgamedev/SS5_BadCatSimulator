using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    public Camera mainCamera;


    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            DetectObject(Input.mousePosition);
        }

        // Check for touch (mobile)
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            DetectObject(Input.touches[0].position);
        }
    }

    void DetectObject(Vector3 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Clicked/Touched Object: " + hit.collider.name);
            // You can also call a function on the object here
            // hit.collider.gameObject.SendMessage("OnClicked", SendMessageOptions.DontRequireReceiver);
        }
    }
}
