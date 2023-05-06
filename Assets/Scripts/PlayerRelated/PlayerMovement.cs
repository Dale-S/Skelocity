using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EventSystems;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public PlayerStats PS;
    //Game Objects
    private Rigidbody playerRB;
    private GameObject player;
    private Transform playerObject; // for easy scaling for sliding
    private Vector3 playerPos;

    //Max Speed Variables
    
    private float maxVelocity;
    private float walkSpeed = 4.0f;
    private float runSpeed = 7.0f;

    //Jump Variables
    private int numOfJumps = 3; //Number of bonus jumps (Ex. 2 = triple jump, 1 = double jump...)
    private int jumps;
    private float groundDetectionHeight = 1.2f;
    private float bufferHeight = 1.85f;
    private float timeToApex = 0.40f;
    private float apex = 3.5f;
    private float jumpVel;

    //Physics variables
    private float gravity;
    public Vector3 movement;
    public Vector3 slopeDir;
    public int dir = 1;

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
    private KeyCode inventoryKey = KeyCode.I;


    //Slide variables
    private float maxSlideTime = 0.4f;
    private float slideForce = 0.075f;
    private float slideTimer;
    private float slideYScale = 0.5f;
    private float startingYScale;

    //Wall Slide and Wall Jump Variables
    private float wallSlideDelay = 3f;
    private float wallJumpTimer = 0f;
    public float wallSlideSpeed = 2f;
    private float savedPlayerVelocity = -1f;
    public float yWallForce;
    public float wallJumpTime;


    //Status Variables
    public bool isGrounded = true;
    private bool jumpBuffer = true;
    private bool sprinting = true;
    private bool spacePressed = false;
    private bool isWallSliding;
    private bool isWallJumping;
    private bool isSliding = false;
    public bool extraJumps = false;
    public bool shortHop = false;
    public bool againstWall = false;
    private bool falling = false;
    public bool slope = false;
    public bool clip = false; //To check for character clipping on objects when jumping

    //Wall detection variables
    private float wallDetectionDist = 0.8f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    public bool wallDebounce = false;

    //Inventory Variables
    private Inventory inventory;
    [SerializeField] private UIInventory uiInventory;
    [SerializeField] private EquippedUI equipUI;

    private void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody>();
        playerObject = GetComponent<Transform>();
        player = this.gameObject;
        playerPos = player.transform.position;
        maxVelocity = PS.defMaxVelocity;
        movement = new Vector3(0, 0, 0);
        gravity = -(2 * apex) / Mathf.Pow(timeToApex, 2);
        jumpVel = Mathf.Abs(gravity) * timeToApex;
        jumps = numOfJumps;
        startingYScale = playerObject.localScale.y;
        againstWall = Physics.Raycast(this.transform.position, Vector3.right, wallDetectionDist);
        slope = Physics.Raycast(this.gameObject.transform.position - new Vector3(0, 0.5f, 0), new Vector3(1, -0.25f, 0), 0.8f);
        clip = Physics.Raycast(this.gameObject.transform.position - new Vector3(0, 0.9f, 0), Vector3.right, wallDetectionDist);

        /*//Start Inventory
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);

        equipUI.SetInventory(inventory);
        equipUI.SetPlayer(this);

        ItemWorld.spawnItemWorld(new Vector3(10, 2, -0.2f),
            new Item { itemType = Item.ItemType.Boots, amount = 1, buffValue = 1.05f });
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld == null) return;
        //Touching Item
        inventory.AddItem(itemWorld.GetItem());
        itemWorld.DestroySelf();
        */
    }

    private void Update()
    {
        if (!isGrounded && againstWall)
        {
            wallStick();
        }
        playerPos = this.transform.position;
        //Check to see if player is on the ground
        isGrounded = Physics.Raycast(this.transform.position, Vector3.down, groundDetectionHeight);
        jumpBuffer = Physics.Raycast(this.transform.position, Vector3.down, bufferHeight);
        slope = Physics.Raycast(this.gameObject.transform.position - new Vector3(0, 0.5f, 0), slopeDir, 0.8f);
        if (pVelocity > 1)
        {
            dir = 1; //Direction = Right
            againstWall = Physics.Raycast(this.transform.position, Vector3.right, wallDetectionDist);
            slopeDir = new Vector3(1, -0.25f, 0);
            clip = Physics.Raycast(this.transform.position - new Vector3(0, 0.9f, 0), Vector3.right, wallDetectionDist);
        }
        else if (pVelocity < -1)
        {
            dir = -1; //Direction = Left
            againstWall = Physics.Raycast(this.transform.position, Vector3.left, wallDetectionDist);
            slopeDir = new Vector3(-1, -0.25f, 0);
            clip = Physics.Raycast(this.gameObject.transform.position - new Vector3(0, 0.9f, 0), Vector3.left, wallDetectionDist);
        }

        if (!againstWall && slope)
        {
            movement.y = 2f;
        }

        //Anti-Clipping---------------------------------------------------------\\
        if (clip && !isGrounded && !againstWall)
        {
            this.transform.position = playerPos + new Vector3(0.3f * dir, 0.5f, 0);
        }
        //----------------------------------------------------------------------\\

        //Jumping Control-------------------------------------------------------\\
        if (!isWallSliding)
        {
            if (Input.GetKeyDown(jumpKey) && jumpBuffer)
            {
                spacePressed = true;
                if (movement.x < maxVelocity && movement.x > -maxVelocity)
                {
                    movement.x = movement.x * 1.05f;
                }

                pVelocity = movement.x;
                if (!wallDebounce)
                {
                    movement.y = jumpVel;
                }
            }

            if (extraJumps == true)
            {
                if (Input.GetKeyDown(jumpKey) && !jumpBuffer)
                {
                    if (jumps > 0)
                    {
                        pVelocity = movement.x;
                        if (!wallDebounce)
                        {
                            movement.y = jumpVel;
                        }
                        jumps--;
                    }
                }
            }

            if (shortHop)
            {
                if (Input.GetKeyUp(jumpKey))
                {
                    if (!falling)
                    {
                        movement.y = gravity * Time.deltaTime;
                    }
                }
            }
        }
        //--------------------------------------------------------------------\\

        //Air Control Code----------------------------------------------------\\
        if (!isGrounded && (Input.GetKey(rightKey) && !Input.GetKey(leftKey)))
            {
                player.transform.localRotation = Quaternion.Euler(0, 0, 0);
                if (movement.x > 0)
                {
                    return;
                }

                if (movement.x < 0)
                {
                    pVelocity = (movement.x * -1);
                }
            }

            if (!isGrounded && (!Input.GetKey(rightKey) && Input.GetKey(leftKey)))
            {
                player.transform.localRotation = Quaternion.Euler(0, -180, 0);
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

        //Check if on Wall and off the ground. If so, slide on the wall.

        //Inventory Toggle-------------------------------------------------------\\
        /*if (Input.GetKeyDown(inventoryKey))
        {
            uiInventory.ChangeInventoryAlpha();
        }*/
        //----------------------------------------------------------------------\\
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            wallDebounce = false;
        }

        //Equipped Items Buff
        //BootsBuff();
        //Walking and Running Control ------------------------------------------\\
        if ((Input.GetKey(rightKey) && !Input.GetKey(leftKey)) && isGrounded)
        {
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
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
            player.transform.localRotation = Quaternion.Euler(0, -180, 0);
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
        
        movement.x = pVelocity;
        
        if (jumpBuffer)
        {
            jumps = numOfJumps;
        }
        if (!isGrounded && !wallDebounce)
        {
            if (movement.y < 0)
            {
                falling = true;
            }

            movement.y += gravity * Time.deltaTime;
            spacePressed = false;
        }
        else if (isGrounded && !spacePressed && !slope)
        {
            falling = false;
            wallDebounce = false;
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
        }
        else if (pVelocity < 0 && pVelocity > -maxVelocity)
        {
            pVelocity -= slideForce;
        }


        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0)
        {
            StopSlide();
        }
    }

    /*public void BootsBuff()
    {
        if (equipUI.equippedBoots == null && maxVelocity != defaultMaxVelocity)
        {
            maxVelocity = defaultMaxVelocity;
        }
        else if (equipUI.equippedBoots != null && maxVelocity == defaultMaxVelocity)
        {
            maxVelocity = defaultMaxVelocity * equipUI.equippedBoots.buffValue;
        }
    }*/
    private void wallStick()
    {
        if (!wallDebounce)
        {
            wallDebounce = true;
            savedPlayerVelocity = pVelocity;
            pVelocity = 0;
            movement.x = 0;
            movement.y = 0;
            Invoke("wallSlide", wallSlideDelay);
        }
    }

    private void wallSlide()
    {
        Debug.Log("sliding down wall");
        if (!isGrounded)
        {
            movement.y = -wallSlideSpeed;
        }
        else
        {
            movement.y = 0;
            wallDebounce = false;
        }
    }
}