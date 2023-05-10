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

    public float enemyHP = 30;
    
    //Enemy Death Sound Effect
    public GameObject deathSoundPrefab;

    //Animator
    Animator animator;

    //Model
    public GameObject enemyModel;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = this.gameObject;
        enemyRB = this.gameObject.GetComponent<Rigidbody>();
        enemyPos = enemy.transform.position;
        animator = enemyModel.GetComponent<Animator>();
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
        if (enemyHP <= 0)
        {
            GameObject deathSound = (GameObject) Instantiate(deathSoundPrefab, transform.position, transform.rotation);
            Debug.Log("Hook Dead");
            Destroy(deathSound, 2f);
            Destroy(this.gameObject);
        }
        if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            Debug.DrawLine(transform.position, playerTransform.position, gizmoColor);
            return;
        }
        edge = Physics.Raycast(enemyPos - new Vector3(0, 0.5f, 0), new Vector3(1 * walkDir, -0.65f, 0), 2f);
        wall = Physics.Raycast(enemyPos - new Vector3(0, 0.8f, 0), new Vector3(1 * walkDir, 0, 0), 1f);
        isGrounded = Physics.Raycast(enemyPos, Vector3.down, 1.2f);
        if (!isGrounded)
        {
            enemyRB.velocity = new Vector3(0, -10f, 0);
        }
        else
        {
            enemyRB.velocity = new Vector3(speed * walkDir, 0, 0);
        }
        animator.SetFloat("Speed", Math.Abs(speed)); 
    }

    void dealtDamage(float damageDealt)
    {
        enemyHP -= damageDealt;
        Debug.Log(enemyHP);
        Debug.Log("Ouch from hook");
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
