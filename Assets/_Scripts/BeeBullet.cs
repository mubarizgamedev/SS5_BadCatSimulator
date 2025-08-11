using System;
using UnityEngine;

public class BeeBullet : MonoBehaviour
{
    public static event Action OnBeeBulletHitGranny;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BeeBullet Triggered with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy"))
        {

            OnBeeBulletHitGranny?.Invoke();
            Destroy(gameObject, 2f);
        }

        Destroy(gameObject, 2f);
    }
}
