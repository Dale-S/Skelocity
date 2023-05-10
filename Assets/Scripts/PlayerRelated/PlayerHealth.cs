using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public PlayerStats PS;
    public PlayerMovement PM;
    private int health;
    private bool invincible = false;
    private float invTime = 2f;
    private float timer;
    public GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        invTime = PS.invinTime;
        health = PS.hearts;
        timer = invTime;
        PS = gameObject.GetComponent<PlayerStats>();
        PM = gameObject.GetComponent<PlayerMovement>();
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
            PM.disabled = true;
            playerModel.SetActive(false);
            SceneManager.LoadScene("Hub");
        }
    }
    
    public void damagePlayer()
    {
        if (!invincible)
        {
            invincible = true;
            Debug.Log("Player damaged");
            health--;
            timer = invTime;
        }
        if (invincible)
        {
            //Debug.Log("Sike you thought");
        }
    }
}
