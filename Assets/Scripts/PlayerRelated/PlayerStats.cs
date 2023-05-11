using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private GameObject player;
    private LevelScript LS;
    private SkillScript SS;
    private int defExtraJumps = 1;
    private float defInvTime = 3f;
    private float defMaxVelocity = 15.0f;
    private float defDamage = 10;
    private float defWalk = 4;
    private float defSprint = 7;
    private float defAttackRange = 5f; 
    private int defHearts = 5;
    private float dAttackCooldown = 2f;
    public float attackCooldown = 2f;
    public float maxVelocity = 15.0f;
    public int hearts = 5;
    public float attackRange = 5;
    public float damage = 5;
    public float sprintSpeed = 7;
    public float walkSpeed = 4;
    public int extraJumps = 1;
    public float invinTime = 3f;
    private float dMulti = 1;
    public float multi = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LS = player.GetComponent<LevelScript>();
        SS = player.GetComponent<SkillScript>();
        extraJumps = defExtraJumps;
        hearts = defHearts;
        damage = defDamage;
        sprintSpeed = defSprint;
        walkSpeed = defWalk;
        extraJumps = defExtraJumps;
        attackRange = defAttackRange;
        invinTime = defInvTime;
        maxVelocity = defMaxVelocity;
        //checkStats();
    }

    void checkStats()
    {/*
        PlayerData data = SaveSystem.LoadPlayer(LS, SS);
        if (data != null)
        {
            attackCooldown = dAttackCooldown - 0.2f * SS.CS3;
            sprintSpeed = defSprint + 0.5f * SS.MS2;
            walkSpeed = defWalk + 0.5f * SS.MS1;
            extraJumps = defExtraJumps + (int)SS.MS3;
            maxVelocity = defMaxVelocity + 0.5f * SS.MS4;
            multi = dMulti + 0.2f * SS.MS5;
            hearts = defHearts + 1 * (int)SS.CS4;
            damage = defDamage + 0.2f * SS.CS1;
            attackRange = defAttackRange + 1 * SS.CS2;
            invinTime = defInvTime + 0.25f * SS.CS5 ;
        }
        */
    }
} 