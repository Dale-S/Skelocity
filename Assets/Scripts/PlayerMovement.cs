using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    //Game Objects
    private Rigidbody playerRB;
    private GameObject player;
    private Vector3 playerPos;
    
    //Max Speed Variables
    private float maxVelocity = 15.0f;
    private float walkSpeed = 4.0f;
    private float runSpeed = 7.0f;

    //Jump Variables
    private int numOfJumps = 1; //Number of bonus jumps (Ex. 2 = triple jump, 1 = double jump...)
    private int jumps;
    private float groundDetectionHeight = 1.2f;
    private float bufferHeight = 1.85f;
    private float timeToApex = 0.40f;
    private float apex = 3.5f;
    private float jumpVel;

    //Physics variables
    private float gravity;
    private Vector3 movement;

    //Players Current Velocity
    public float pVelocity = 0;

    //Walk and Run Speed Increase (For speed ramp up)
    private float speedIncWalk = 0.25f;
    private float speedIncRun = 0.50f;
    
    //Status Variables
    private bool isGrounded = true;
    private bool jumpBuffer = true;
    private bool spacePressed = false;
    public bool extraJumps = false;


    private void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody>();
        player = this.gameObject;
        playerPos = player.transform.position;
        movement = new Vector3(0, 0, 0);
        gravity = -(2 * apex) / Mathf.Pow(timeToApex, 2);
        jumpVel = Mathf.Abs(gravity) * timeToApex;
        jumps = numOfJumps;
    }

    private void Update()
    {
        //Check to see if player is on the ground
        isGrounded = Physics.Raycast(this.transform.position, Vector3.down, groundDetectionHeight);
        jumpBuffer = Physics.Raycast(this.transform.position, Vector3.down, bufferHeight);

        //Jumping Control-------------------------------------------------------\\
        if (Input.GetKeyDown(KeyCode.Space) && jumpBuffer)
        {
            spacePressed = true;
            movement.x = movement.x * 1.2f;
            pVelocity = movement.x;
            movement.y = jumpVel;
        }

        if (extraJumps == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !jumpBuffer)
            {
                if (jumps > 0)
                {
                    movement.x = movement.x * 1.2f;
                    pVelocity = movement.x;
                    movement.y = jumpVel;
                    jumps--;
                }
            }
        }
        //----------------------------------------------------------------------\\
    }
    void FixedUpdate()
    {
        //Walking and Running Control ------------------------------------------\\
        if ((Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (pVelocity < runSpeed)
                {
                    pVelocity += speedIncRun;
                    movement.x = pVelocity;
                }
            }
            else
            {
                if (pVelocity < walkSpeed)
                {
                    pVelocity += speedIncWalk;
                    movement.x = pVelocity;
                }
            }
        }
        
        if ((Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (pVelocity > -runSpeed)
                {
                    pVelocity -= speedIncRun;
                    movement.x = pVelocity;
                }
            }
            else
            {
                if (pVelocity > -walkSpeed)
                {
                    pVelocity -= speedIncWalk;
                    movement.x = pVelocity;
                }
            }
        }

        if ((!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) && isGrounded)
        {
            if (pVelocity > 0.00f)
            {
                pVelocity -= speedIncWalk;
                movement.x = pVelocity;
            }
            if (pVelocity < 0.00f)
            {
                pVelocity += speedIncWalk;
                movement.x = pVelocity;
            }
            //Debug.Log("Slowing Down...");
        }
        //----------------------------------------------------------------------\\

        //Player movement update
        if (jumpBuffer)
        {
            jumps = numOfJumps;
        }
        if (!isGrounded)
        {
            movement.y += gravity * Time.deltaTime;
            spacePressed = false;
        }
        else if (isGrounded && !spacePressed)
        {
            movement.y = 0;
        }
        playerRB.velocity = movement;
        //Debug.Log("Current velocity: " + pVelocity);
    }

    
}
