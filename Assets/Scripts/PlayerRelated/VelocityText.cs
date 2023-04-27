using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VelocityText : MonoBehaviour
{
    public PlayerMovement PM;
    public TextMeshProUGUI velText;
    private float vel;

    // Update is called once per frame
    void Update()
    {
        vel = PM.pVelocity;
        velText.text = "Current Velocity: " + Mathf.Abs(Mathf.Round(vel));
    }
}
