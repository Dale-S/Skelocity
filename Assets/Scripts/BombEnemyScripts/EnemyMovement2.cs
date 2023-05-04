using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class EnemyMovement2 : MonoBehaviour
{
    public float speed = 1.0f;
    public bool edge;
    public bool wall;
    private float walkDir = 1.0f;
    private Rigidbody enemyRB;
    private GameObject enemy;
    private Vector3 enemyPos;
    public bool isGrounded;
    
    public GameObject player;
    public GameObject explosionPrefab;

    public float distanceThreshold = 5f;
    public float explosionRadius = 3f;
    public float explosionDelay = 3f;

    private bool isExploded = false;
    private bool playerDetected = false;
    private bool enemyStop = false;
    private float distanceToPlayer;

    private void Start()
    {
        enemy = gameObject;
        enemyRB = gameObject.GetComponent<Rigidbody>();
        enemyPos = enemy.transform.position;
        
    }

    private void FixedUpdate()
    {
        enemyPos = enemy.transform.position;
        
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        if ((wall || !edge) && isGrounded)
        {
            if (walkDir > 0)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
            }
            if (walkDir < 0)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0,180,0);
            }
            walkDir = -walkDir;
        }
    }

    private void Update()
    {
        edge = Physics.Raycast(enemyPos - new Vector3(0, 0.5f, 0), new Vector3(1 * walkDir, -0.75f, 0), 1f);
        wall = Physics.Raycast(enemyPos - new Vector3(0, 0.8f, 0), new Vector3(1 * walkDir, 0, 0), 0.8f);
        isGrounded = Physics.Raycast(enemyPos, Vector3.down, 1.2f);
        
        if (distanceToPlayer <= distanceThreshold)
        {
            playerDetected = true;
            
            if (!enemyStop)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (speed * 2) * Time.deltaTime);
            }
            
            enemyRB.velocity = new Vector3(0f, 0f, 0f);
        }
        
        if (!playerDetected)
        {
            if (!isGrounded)
            {
                enemyRB.velocity = new Vector3(0, -10f, 0);
            }
            else
            {
                enemyRB.velocity = new Vector3(speed * walkDir, 0, 0);
            }
        }
        
        Explosion();
    }

    void Explosion()
    {
        if (!isExploded)
        {
            if (distanceToPlayer < explosionRadius)
            {
                enemyStop = true;
                Invoke("Explode", explosionDelay);
                isExploded = true;
            }

            playerDetected = false;
        }
    }

    void Explode()
    {
        GameObject effectIns = (GameObject) Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceThreshold);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
