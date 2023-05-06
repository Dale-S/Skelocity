using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public float speed = 1.0f;
    public bool edge;
    public bool wall;
    private float walkDir = 1.0f;
    private Rigidbody enemyRB;
    private GameObject enemy;
    private Vector3 enemyPos;
    public bool isGrounded;
    public float enemyHP = 20;

    private void Start()
    {
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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bonk") )
        {
            Debug.Log(enemyHP);
            enemyHP -= 10;
            Debug.Log("Ouch from shooter");
        }
    }
    
    
    private void Update()
    {
        if (enemyHP <= 0)
        {
            Debug.Log("Shooter Dead");
            Destroy(this.gameObject);
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
}