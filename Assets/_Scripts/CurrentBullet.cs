using System;
using UnityEngine;

public class CurrentBullet : MonoBehaviour
{
    public static event Action OnCurrentBulletHitGranny;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BeeBullet Triggered with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy"))
        {
            OnCurrentBulletHitGranny?.Invoke();
            Destroy(gameObject, 2f);
        }

        Destroy(gameObject, 2f);
    }
}
