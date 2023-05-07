using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStats PS;
    public PlayerMovement PM;
    public float damageDealt = 0f;
    private bool attackCooldown = false;
    private float dTimer = 2f;
    private float timer = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !attackCooldown)
        {
            attack();
        }

        if (attackCooldown)
        {
            timer = timer - Time.deltaTime;
        }

        if (timer <= 0)
        {
            attackCooldown = false;
        }
    }

    private void attack()
    {
        timer = dTimer;
        attackCooldown = true;
        damageDealt = PS.damage * Mathf.Abs(PM.pVelocity);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(PM.dir, 0, 0), out hit, PS.attackRange))
        {
            hit.transform.SendMessage("dealtDamage", damageDealt);
        }
    }
}
