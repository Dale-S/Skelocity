using System;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth PH;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SendMessage("damagePlayer");
            Debug.Log("I shot you");
        }
        if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}