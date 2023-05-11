using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private GameObject player;
    private LevelScript LS;
    private SkillScript SS;
    private float saveTime = 10;
    private float timer = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LS = player.GetComponent<LevelScript>();
        SS = player.GetComponent<SkillScript>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            autoSave();
            timer = saveTime;
        }
    }

    public void SaveData()
    {
        SaveSystem.SavePlayer(LS,SS);
    }

    public void autoSave()
    {
        SaveData();
        Debug.Log("Progress Saved");
    }
}
