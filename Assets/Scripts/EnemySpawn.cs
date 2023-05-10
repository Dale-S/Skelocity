using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private GameObject levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager");
        levelManager.GetComponent<EnemyCount>().count += 1;
    }

    /*private void Update() //for testing purposes
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            levelManager.GetComponent<EnemyCount>().count -= 1;
            Destroy(this.gameObject);
        }
    }*/
}
