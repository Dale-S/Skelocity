using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int defExtraJumps = 1;
    public float defInvTime = 3f;
    public float defMaxVelocity = 15.0f;
    public float defDamage = 10;
    public float defWalk = 4;
    public float defSprint = 7;
    public float defAttackRange = 5f; 
    public int defHearts = 5;
    public float maxVelocity = 15.0f;
    public int hearts = 5;
    public float attackRange = 3;
    public float damage = 5;
    public float sprintSpeed = 7;
    public float walkSpeed = 4;
    public int extraJumps = 1;
    public float invinTime = 3f;
    public float multi = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        extraJumps = defExtraJumps;
        hearts = defHearts;
        damage = defDamage;
        sprintSpeed = defSprint;
        walkSpeed = defWalk;
        extraJumps = defExtraJumps;
        attackRange = defAttackRange;
        invinTime = defInvTime;
        maxVelocity = defMaxVelocity;
    }
} 