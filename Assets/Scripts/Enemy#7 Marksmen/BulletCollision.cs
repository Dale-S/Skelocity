using System;
using TreeEditor;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth PH;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            PH = player.GetComponent<PlayerHealth>();
            PH.damagePlayer();
            Debug.Log("I shot you");
        }
        if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}