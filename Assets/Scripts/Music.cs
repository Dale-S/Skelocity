using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip slow;
    public AudioClip fast;
    public AudioClip faster;
    public AudioSource sound;
    private GameObject player;
    private PlayerMovement PM;
    private bool swap = true;
    private bool change = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PM = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        sound.clip = faster;
        /*
        if (PM.pVelocity <= 4)
        {
            sound.clip = slow;
            swap = true;
        }
        else if (PM.pVelocity > 4 && PM.pVelocity <= 10)
        {
            sound.clip = fast;
            swap = true;
        }
        else if (PM.pVelocity > 10 && PM.pVelocity <= 15)
        {
            sound.clip = faster;
            swap = true;
        }
        */
        if (swap)
        {
            sound.Play();
            swap = false;
        }
    }
}
