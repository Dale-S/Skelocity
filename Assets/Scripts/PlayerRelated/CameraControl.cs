using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject cam;
    public GameObject player;
    public PlayerMovement PM;
    public float vel = 0f;

    void Update()
    {
        if (!PM.wallStickActive)
        { 
            vel = PM.pVelocity;
            if (vel < 0)
            {
                vel = -vel;
            }
            cam.transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + 4), -10f);
            cam.GetComponent<Camera>().fieldOfView = 60 + vel;
        }
        else
        {
            cam.transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + 4), -10f);
            cam.GetComponent<Camera>().fieldOfView = 60 + Mathf.Abs(PM.savedSpeed);
        }
    }
}
