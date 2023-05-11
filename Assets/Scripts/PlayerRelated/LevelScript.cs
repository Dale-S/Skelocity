using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public int movementLVL;
    public int combatLVL;
    public int xpToNext1; //Movement
    public int xpToNext2; //Combat
    public int currXP1;
    public int currXP2;
    public int skillPoints1; //Movement
    public int skillPoints2; //Combat
    private PlayerMovement PM;
    private GameObject player;
    private float currSpeed;
    private float defTick = 5f;
    private float tick1 = 5f;
    private float tick2 = 5f;
    private float tick3 = 5f;
    private PlayerStats PS;
    public Image mask1;
    public Image mask2;
    
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        PM = player.GetComponent<PlayerMovement>();
        PS = player.GetComponent<PlayerStats>();
        
        //Data to be saved
        movementLVL = 1;
        combatLVL = 1;
        xpToNext1 = 30 * movementLVL;
        xpToNext2 = 15 * combatLVL;
        currXP1 = 0; //For movement
        currXP2 = 0; //For combat
        skillPoints1 = 0;
        skillPoints2 = 0;
        ////////////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Level Code
        currSpeed = PM.pVelocity;
        if (currSpeed >= 3 && currSpeed < 6)
        {
            tick1 -= Time.deltaTime;
            if (tick1 <= 0)
            {
                currXP1 += 1;
                tick1 = defTick;
            }
        }
        else if (currSpeed >= 6 && currSpeed < 12)
        {
            tick2 -= Time.deltaTime;
            if (tick2 <= 0)
            {
                currXP1 += 3;
                tick2 = defTick;
            }
        }
        else if (currSpeed >= 12)
        {
            tick3 -= Time.deltaTime;
            if (tick3 <= 0)
            {
                currXP1 += 5;
                tick3 = defTick;
            }
        }
        ////////////////////////////
        //Check xp for Level Up
        if (currXP1 >= xpToNext1)
        {
            currXP1 = currXP1 % xpToNext1;
            movementLVL += 1;
            xpToNext1 = 30 * movementLVL;
            skillPoints1 += 1;
        }
        
        GetFillAmount1();
        GetFillAmount2();
    }

    public void addXP() //This function is for combat xp only;
    {
        currXP2 += 5;
        if (currXP2 >= xpToNext2)
        {
            currXP2 = currXP2 % xpToNext1;
            combatLVL += 1;
            xpToNext2 = 15 * combatLVL;
            skillPoints2 += 1;
        }
    }

    void GetFillAmount1()
    {
        float fillAmount = (float)currXP1 / (float)xpToNext1;
        mask1.fillAmount = fillAmount;
    }
    
    void GetFillAmount2()
    {
        float fillAmount = (float)currXP2 / (float)xpToNext2;
        mask2.fillAmount = fillAmount;
    }
}