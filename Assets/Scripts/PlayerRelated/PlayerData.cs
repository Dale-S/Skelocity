using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerData : MonoBehaviour
{
    public int movementLVL;
    public int combatLVL;
    public int skillPoints1; //Movement
    public int skillPoints2; //Combat
    public int currXP1;
    public int currXP2;
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

    public PlayerData(LevelScript LS, SkillScript SS)
    {
        movementLVL = LS.movementLVL;
        combatLVL = LS.combatLVL;
        skillPoints1 = LS.skillPoints1;
        skillPoints2 = LS.skillPoints2;
        currXP1 = LS.currXP1;
        currXP2 = LS.currXP2;
        MS1 = SS.MS1;
        MS2 = SS.MS2;
        MS3 = SS.MS3;
        MS4 = SS.MS4;
        MS5 = SS.MS5;
        CS1 = SS.CS1;
        CS2 = SS.CS2;
        CS3 = SS.CS3;
        CS4 = SS.CS4;
        CS5 = SS.CS5;
    }
}
