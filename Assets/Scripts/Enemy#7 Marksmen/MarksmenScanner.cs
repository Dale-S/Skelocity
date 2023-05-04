using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksmenScanner : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float burstInterval = 0.2f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int bulletsInBurst = 3;

    private bool playerSeen = false;
    private bool isShooting = false;

    void Update()
    {
        ScanForPlayer();

        if (playerSeen && !isShooting)
        {
            StartCoroutine(ShootBurst());
        }
    }

    void ScanForPlayer()
    {
        RaycastHit hit;
        Vector3 xAxisDirection = -transform.right; // This sets the direction to the local x-axis
        bool playerInRange = Physics.Raycast(transform.position, xAxisDirection, out hit, detectionRange, playerLayer);

        if (playerInRange && hit.collider.CompareTag("Player"))
        {
            if (!playerSeen)
            {
                playerSeen = true;
                Debug.Log("Enemy spotted");
                if (!isShooting)
                {
                    StartCoroutine(ShootBurst());
                }
            }
        }
        else
        {
            if (playerSeen)
            {
                playerSeen = false;
                Debug.Log("Enemy gone");
            }
        }
    }

    IEnumerator ShootBurst()
    {
        isShooting = true;

        // First shot
        ShootBullet();

        // Short delay before the second shot
        yield return new WaitForSeconds(burstInterval * 0.5f);

        // Second shot
        ShootBullet();

        // Longer delay before the third shot
        yield return new WaitForSeconds(burstInterval * 1.5f);

        // Third shot
        ShootBullet();

        isShooting = false;
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = -transform.right * bulletSpeed;

        // Set bullet as trigger and add a script to handle the collision
        bullet.GetComponent<Collider>().isTrigger = true;
        bullet.AddComponent<BulletCollision>();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = playerSeen ? Color.red : Color.yellow;
        Gizmos.DrawRay(transform.position, -transform.right * detectionRange);
    }
}
