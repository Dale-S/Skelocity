using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    private GameObject player;
    private LevelScript LS;
    private SkillScript SS;
    public float MS1 = 0f;
    public float MS2 = 0f;
    public float MS3 = 0f;
    public float MS4 = 0f;
    public float MS5 = 0f;
    public float CS1 = 0f;
    public float CS2 = 0f;
    public float CS3 = 0f;
    public float CS4 = 0f;
    public float CS5 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LS = player.GetComponent<LevelScript>();
        SS = player.GetComponent<SkillScript>();
        //LoadSkills();
    }

    public void BuyMovementSkill(int i)
    {
        if (LS.skillPoints1 < 1)
        {
            return;
        }
        if (i == 1)
        {
            if (MS1 < 5)
            {
                LS.skillPoints1--;
                MS1++;
            }
        }
        else if (i == 2)
        {
            if (MS2 < 5)
            {
                LS.skillPoints1--;
                MS2++;
            }
        }
        else if (i == 3)
        {
            if (MS3 < 5)
            {
                LS.skillPoints1--;
                MS3++;
            }
        }
        else if (i == 4)
        {
            if (MS4 < 5)
            {
                LS.skillPoints1--;
                MS4++;
            }
        }
        else
        {
            if (MS5 < 5)
            {   
                LS.skillPoints1--;
                MS5++;
            }
        }
    }
    
    public void BuyCombatSkill(int i)
    {
        if (LS.skillPoints2 == 0)
        {
            return;
        }
        if (i == 1)
        {
            if (CS1 < 5)
            {
                LS.skillPoints2--;
                CS1++;
            }
        }
        else if (i == 2)
        {
            if (CS2 < 5)
            {
                LS.skillPoints2--;
                CS2++;
            }
        }
        else if (i == 3)
        {
            if (CS3 < 5)
            {
                LS.skillPoints2--;
                CS3++;
            }
        }
        else if (i == 4)
        {
            if (CS4 < 5)
            {
                LS.skillPoints2--;
                CS4++;
            }
        }
        else
        {
            if (CS5 < 5)
            {
                LS.skillPoints2--; 
                CS5++;
            }
        }
    }

    public void LoadSkills()
    {
        /*PlayerData data = SaveSystem.LoadPlayer(LS, this);
        if(data != null)
        {
            MS1 = data.MS1;
            MS2 = data.MS2;
            MS3 = data.MS3;
            MS4 = data.MS4;
            MS5 = data.MS5;
            CS1 = data.CS1;
            CS2 = data.CS2;
            CS3 = data.CS3;
            CS4 = data.CS4;
            CS5 = data.CS5;
        }
        */
    }
}
