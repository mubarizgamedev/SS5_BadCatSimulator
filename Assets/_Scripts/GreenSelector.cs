using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSelector : MonoBehaviour
{
    public GameObject greenSelector;

    public void ChangeActiveState(bool cond)
    {
        greenSelector.SetActive(cond);
    }
}
