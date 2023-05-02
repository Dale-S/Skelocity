using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    //Game Objects
    private Rigidbody playerRB;
    private GameObject player;
    private Transform playerObject; // for easy scaling for sliding
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
    private float jumpBoost = 1.05f;
    private float jumpVel;

    //Physics variables
    private float gravity;
    private Vector3 movement;

    //Players Current Velocity
    public float pVelocity = 0;

    //Walk and Run Speed Increase (For speed ramp up)
    private float speedIncWalk = 0.25f;
    private float speedIncRun = 0.50f;
    
    //Control Definitions
    private KeyCode slideKey = KeyCode.LeftControl;
    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode sprint = KeyCode.LeftShift;

    //Slide variables
    private float maxSlideTime = 0.4f;
    private float slideForce = 0.075f;
    private float slideTimer;
    private float slideYScale = 0.5f;
    private float startingYScale;

    //Status Variables
    private bool isGrounded = true;
    private bool jumpBuffer = true;
    private bool sprinting = true;
    private bool spacePressed = false;
    private bool isSliding = false;
    public bool extraJumps = false;
    public bool againstWall = false;
    private bool falling = false;

    //Wall detection variables
    private float wallDetectionDist = 1.5f;

    //Inventory Variables
    private Inventory inventory;
    [SerializeField] private UIInventory uiInventory;
    
    private void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody>();
        playerObject = GetComponent<Transform>();
        player = this.gameObject;
        playerPos = player.transform.position;
        movement = new Vector3(0, 0, 0);
        gravity = -(2 * apex) / Mathf.Pow(timeToApex, 2);
        jumpVel = Mathf.Abs(gravity) * timeToApex;
        jumps = numOfJumps;
        startingYScale = playerObject.localScale.y;
        
        //Start Inventory
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld == null) return;
        //Touching Item
        inventory.AddItem(itemWorld.GetItem());
        itemWorld.DestroySelf();
    }

    private void Update()
    {
        //Check to see if player is on the ground
        isGrounded = Physics.Raycast(this.transform.position, Vector3.down, groundDetectionHeight);
        jumpBuffer = Physics.Raycast(this.transform.position, Vector3.down, bufferHeight);
        againstWall = Physics.Raycast(this.transform.position, Vector3.forward, wallDetectionDist);
        
        

        //Jumping Control-------------------------------------------------------\\
        if (Input.GetKeyDown(jumpKey) && jumpBuffer)
        {
            spacePressed = true;
            if (movement.x < maxVelocity && movement.x > -maxVelocity)
            {
                movement.x = movement.x * 1.05f;
            }
            pVelocity = movement.x;
            movement.y = jumpVel;
        }

        if (extraJumps == true)
        {
            if (Input.GetKeyDown(jumpKey) && !jumpBuffer)
            {
                if (jumps > 0)
                {
                    pVelocity = movement.x;
                    movement.y = jumpVel;
                    jumps--;
                }
            }
        }

        if (Input.GetKeyUp(jumpKey))
        {
            if (!falling)
            {
                movement.y = gravity * Time.deltaTime;
            }
        }
        
        //Air Control Code----------------------------------------------------\\
        if(!isGrounded && (Input.GetKey(rightKey) && !Input.GetKey(leftKey)))
        {
            if (movement.x > 0)
            {
                return;
            }

            if (movement.x < 0)
            {
                pVelocity = (movement.x * -1);
            }
        }
        
        if(!isGrounded && (!Input.GetKey(rightKey) && Input.GetKey(leftKey)))
        {
            if (movement.x < 0)
            {
                return;
            }

            if (movement.x > 0)
            {
                pVelocity = (movement.x * -1);
            }
        }
        //----------------------------------------------------------------------\\
        
        //Sliding Control-------------------------------------------------------\\
        if (Input.GetKeyDown(slideKey) && isGrounded)
        {
            StartSlide();
        }

        if (Input.GetKeyUp(slideKey) && isSliding || !isGrounded)
        {
            StopSlide();
        }
        //----------------------------------------------------------------------\\
    }
    void FixedUpdate()
    {
        //Walking and Running Control ------------------------------------------\\
        if ((Input.GetKey(rightKey) && !Input.GetKey(leftKey)) && isGrounded)
        {
            player.transform.localRotation = Quaternion.Euler(0,0,0);
            if (Input.GetKey(sprint))
            {
                sprinting = true;
            }
            else
            {
                sprinting = false;
            }
            
            if (sprinting)
            {
                if (pVelocity < runSpeed)
                {
                    pVelocity += speedIncRun;
                }
            }
            else if (!sprinting)
            {
                if (pVelocity < walkSpeed)
                {
                    pVelocity += speedIncWalk;
                }

                if (pVelocity > walkSpeed)
                {
                    pVelocity -= speedIncWalk;
                }
            }
        }
        
        if ((Input.GetKey(leftKey) && !Input.GetKey(rightKey)) && isGrounded)
        {
            player.transform.localRotation = Quaternion.Euler(0,-180,0);
            if (Input.GetKey(sprint))
            {
                sprinting = true;
            }
            else
            {
                sprinting = false;
            }

            if (sprinting)
            {
                if (pVelocity > -runSpeed)
                {
                    pVelocity -= speedIncRun;
                }
            }
            else if (!sprinting)
            {
                if (pVelocity > -walkSpeed)
                {
                    pVelocity -= speedIncWalk;
                }
                if (pVelocity > walkSpeed)
                {
                    pVelocity += speedIncWalk;
                }
            }
        }

        if ((!Input.GetKey(rightKey) && !Input.GetKey(leftKey)) && isGrounded)
        {
            if (pVelocity > 0.00f)
            {
                pVelocity -= speedIncWalk;
            }
            if (pVelocity < 0.00f)
            {
                pVelocity += speedIncWalk;
            }
            //Debug.Log("Slowing Down...");
        }
        //----------------------------------------------------------------------\\

        //Player sliding update
        if (isSliding)
        {
            Slide(); 
        }
        
        //Player movement update (!|**Keep At Bottom Of Fixed Update**|!)
        //Update Players Current Velocity to pVelocity
        movement.x = pVelocity;
        if (jumpBuffer)
        {
            jumps = numOfJumps;
        }
        if (!isGrounded)
        {
            if (movement.y < 0)
            {
                falling = true;
            }
            movement.y += gravity * Time.deltaTime;
            spacePressed = false;
        }
        else if (isGrounded && !spacePressed)
        {
            movement.y = 0;
        }
        playerRB.velocity = movement;
        //Debug.Log("Current velocity: " + pVelocity)
    }

    private void StartSlide()
    {
        isSliding = true;

        playerObject.localScale = new Vector3(playerObject.localScale.x, slideYScale, playerObject.localScale.z);
        //playerRB.AddForce(Vector3.down * 1f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void StopSlide()
    {
        isSliding = false;
        playerObject.localScale = new Vector3(playerObject.localScale.x, startingYScale, playerObject.localScale.z);
    }

    private void Slide()
    {
        if (pVelocity > 0 && pVelocity < maxVelocity)
        {
            pVelocity += slideForce;
        } else if (pVelocity < 0 && pVelocity > -maxVelocity)
        {
            pVelocity -= slideForce;
        }
        
        
        slideTimer -= Time.deltaTime;
        
        if (slideTimer <= 0)
        {
            StopSlide();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
