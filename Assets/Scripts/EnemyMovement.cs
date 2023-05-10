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
    private float walkDir = 1f;
    private Rigidbody enemyRB;
    private GameObject enemy;
    private Vector3 enemyPos;
    public bool isGrounded;
    public bool completelyGrounded;
    public float enemyHP = 20;
    private bool ray1;
    private bool ray2;
    private bool ray3;

    //Enemy Death Effect
    public GameObject deathSoundPrefab;

    //Animator
    Animator animator;

    //Model
    public GameObject enemyModel;

    private void Start()
    {
        enemy = this.gameObject;
        enemyRB = this.gameObject.GetComponent<Rigidbody>();
        enemyPos = enemy.transform.position;
        animator = enemyModel.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        edge = Physics.Raycast(enemyPos - new Vector3(0, 0.5f, 0), new Vector3(1 * walkDir, -0.75f, 0), 2f);
        wall = Physics.Raycast(enemyPos - new Vector3(0, 0.8f, 0), new Vector3(1 * walkDir, 0, 0), 1f);
        ray1 = Physics.Raycast(enemyPos - new Vector3(0.4f, 0, 0), Vector3.down, 1.2f);
        ray2 = Physics.Raycast(enemyPos, Vector3.down, 1.2f);
        ray3 = Physics.Raycast(enemyPos + new Vector3(0.4f, 0, 0), Vector3.down, 1.2f);
        enemyPos = enemy.transform.position;
        if ((wall || !edge || !completelyGrounded) && isGrounded)
        {
            walkDir = -walkDir;
            if (walkDir > 0)
            {
                this.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
            }
            if (walkDir < 0)
            {
                this.gameObject.transform.localRotation = Quaternion.Euler(0,180,0);
            }
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
            GameObject deathSound = (GameObject) Instantiate(deathSoundPrefab, transform.position, transform.rotation);
            Debug.Log("Shooter Dead");
            Destroy(deathSound, 2f);
            Destroy(this.gameObject);
        }

        if(ray1||ray2||ray3){
            isGrounded = true;
        }
        else{
            isGrounded = false;
        }

        if(ray1&&ray2&&ray3){
            completelyGrounded = true;
        }
        else{
           completelyGrounded = false;
        }

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
}