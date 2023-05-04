using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHud : MonoBehaviour
{
    public GameObject skillTreePanel;

    private void Start()
    {
        skillTreePanel.SetActive(!skillTreePanel.activeSelf);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            skillTreePanel.SetActive(!skillTreePanel.activeSelf);
        }
    }   
}
