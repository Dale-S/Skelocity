using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    public static SoundAssets Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    public AudioClip explosion;
}
