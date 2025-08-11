using UnityEngine;
using System;

public class FireBullet : MonoBehaviour
{
    public static event Action OnFireBulletHitGranny;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BeeBullet Triggered with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy"))
        {
            OnFireBulletHitGranny?.Invoke();
            Destroy(gameObject, 2f);
        }

        Destroy(gameObject, 2f);
    }
}
