using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats PS;
    private int health;
    private bool invincible = false;
    private float invTime = 2f;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        health = PS.hearts;
        timer = invTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            timer = timer - Time.deltaTime;
            //Debug.Log(timer);
            if (timer <= 0)
            {
                invincible = false;
                timer = invTime;
            }
        }
        if (health <= 0)
        {
            //Kill player and send back to the hub
        }
    }

    void damagePlayer()
    {
        if (!invincible)
        {
            Debug.Log("Player damaged");
            health--;
            invincible = true;
            timer = invTime;
        }
        if (invincible)
        {
            Debug.Log("Sike you thought");
        }
    }
}
