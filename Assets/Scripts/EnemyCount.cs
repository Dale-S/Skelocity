using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public int count = 0;
    private int i;
    public GameObject portal;
    private Vector3 portalPlacement;
    private PlayerMovement PM;
    private GameObject player;
    private bool started = false;
    private bool spawned = false;
    private float placementX;
    private float placementY;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PM = player.GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        if (count > 0 && !started)
        {
            started = true;
        }

        if (count <= 0 && started)
        {
            if (PM.dir > 0)
            {
                placementX = player.transform.position.x - 1f;
                placementY = player.transform.position.y + 0.5f;
            }
            else
            {
                placementX = player.transform.position.x + 1f;
                placementY = player.transform.position.y + 0.5f;
            }
            
            if (!spawned && PM.isGrounded && !PM.againstWall)
            {
                portalPlacement = new Vector3(placementX, placementY, player.transform.position.z);
                Instantiate(portal, portalPlacement, Quaternion.identity);
                spawned = true;
            }
        }
        
        i++;
        if (i % 1000 == 0)
        {
            Debug.Log("Enemies Left: " + count);
        }
    }
}
