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
    private float invTime = 3f;
    private float timer;
    public GameObject playerModel;
    //public Renderer playerModels;
    public Renderer hoodModel;
    public Renderer armorModel;
    public Renderer skirtModel;
    public Renderer beltModel;
    public Material damagedMaterial;

    //private Material skeletonMaterial;
    private Material armorMaterial;
    private Material skirtMaterial;
    private Material beltMaterial;
    private Material hoodMaterial;

    // Start is called before the first frame update
    void Start()
    {
        invTime = PS.invinTime;
        health = PS.hearts;
        timer = invTime;
        PS = gameObject.GetComponent<PlayerStats>();
        PM = gameObject.GetComponent<PlayerMovement>();

        //skeletonMaterial = playerModel.GetComponent<MeshRenderer>().material;
        armorMaterial = armorModel.GetComponent<Renderer>().materials[2];
        skirtMaterial = skirtModel.GetComponent<Renderer>().material;
        beltMaterial = beltModel.GetComponent<Renderer>().material;
        hoodMaterial = hoodModel.GetComponent<Renderer>().material;
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
            //PM.disabled = true;
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
            StartCoroutine(DamageFlash());
        }
    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < 5; i++)
        {
            //playerModels.material = damagedMaterial;
            hoodModel.material = damagedMaterial;
            armorModel.material = damagedMaterial;
            skirtModel.material = damagedMaterial;
            beltModel.material = damagedMaterial;

            yield return new WaitForSeconds(0.1f);

            //playerModels.material = damagedMaterial;
            hoodModel.material = hoodMaterial;
            armorModel.material = armorMaterial;
            skirtModel.material = skirtMaterial;
            beltModel.material = beltMaterial;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
