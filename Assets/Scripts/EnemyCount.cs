using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public int count = 0;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Update()
    {
        i++;
        if (i % 1000 == 0)
        {
            Debug.Log("Enemies Left: " + count);
        }
    }
}
