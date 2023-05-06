using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class EnemyMoveHook : MonoBehaviour
{
    public float detectionRadius = 3f;
    public Color gizmoColor = Color.green;
    public float speed = 1.0f;
    public bool edge;
    public bool wall;
    private float walkDir = 1.0f;
    private Rigidbody enemyRB;
    private GameObject enemy;
    private Vector3 enemyPos;
    public bool isGrounded;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        enemy = this.gameObject;
        enemyRB = this.gameObject.GetComponent<Rigidbody>();
        enemyPos = enemy.transform.position;
    }

    private void FixedUpdate()
    {
        enemyPos = enemy.transform.position;
        if ((wall || !edge) && isGrounded)
        {
            if (walkDir > 0)
            {
                this.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
            }
            if (walkDir < 0)
            {
                this.gameObject.transform.localRotation = Quaternion.Euler(0,180,0);
            }
            walkDir = -walkDir;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            Debug.DrawLine(transform.position, playerTransform.position, gizmoColor);
            return;
        }
        edge = Physics.Raycast(enemyPos - new Vector3(0, 0.5f, 0), new Vector3(1 * walkDir, -0.75f, 0), 1f);
        wall = Physics.Raycast(enemyPos - new Vector3(0, 0.8f, 0), new Vector3(1 * walkDir, 0, 0), 0.8f);
        isGrounded = Physics.Raycast(enemyPos, Vector3.down, 1.2f);
        if (!isGrounded)
        {
            enemyRB.velocity = new Vector3(0, -10f, 0);
        }
        else
        {
            enemyRB.velocity = new Vector3(speed * walkDir, 0, 0);
        }
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
