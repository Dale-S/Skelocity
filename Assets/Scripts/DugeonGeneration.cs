using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DugeonGeneration : MonoBehaviour
{
    public int dungeonSize = 3;
    public GameObject[] All4;
    public GameObject[] LB;
    public GameObject[] LRB;
    public GameObject[] LT;
    public GameObject[] LTB;
    public GameObject[] LTR;
    public GameObject[] RB;
    public GameObject[] TR;
    public GameObject[] TRB;
    
    private float x;
    private float y;
    private float z;
    private Vector3 currPoint;
    
    void Start()
    {
        x = 0;
        y = -30 * (dungeonSize / 2);
        z = 0;
        currPoint = new Vector3(x, y, z);
        for (int i = 0; i < dungeonSize; i++)
        {
            for (int j = 0; j < dungeonSize; j++)
            {
                currPoint = new Vector3(x, y, z);
                //Debug.Log("J = " + j + "  I = " + i);
                //Debug.Log("Current Point = " + currPoint);
                if (currPoint.y == 0 && currPoint.x == 0)
                {
                    int rand = 0;
                    rand = Random.Range(0, All4.Length);
                    Instantiate(All4[rand], currPoint, Quaternion.identity);
                    //Debug.Log("All4 Instantiated");
                }
                else if (currPoint.y == -30 * (dungeonSize / 2))
                {
                    if (currPoint.x == 0)
                    {
                        int rand = 0;
                        rand = Random.Range(0, TR.Length);
                        Instantiate(TR[rand], currPoint, Quaternion.identity);
                        //Debug.Log("TR Instantiated");
                    }
                    else if (currPoint.x == 49.75 * (dungeonSize - 1))
                    {
                        int rand = 0;
                        rand = Random.Range(0, LT.Length);
                        Instantiate(LT[rand], currPoint, Quaternion.identity);
                        //Debug.Log("LT Instantiated");
                    }
                    else
                    {
                        int rand = 0;
                        rand = Random.Range(0, LTR.Length);
                        Instantiate(LTR[rand], currPoint, Quaternion.identity);
                        //Debug.Log("LTR Instantiated");
                    }
                }
                else if (currPoint.y == 30 * (dungeonSize / 2))
                {
                    if (currPoint.x == 0)
                    {
                        int rand = 0;
                        rand = Random.Range(0, RB.Length);
                        Instantiate(RB[rand], currPoint, Quaternion.identity);
                        //Debug.Log("RB Instantiated");
                    }
                    else if (currPoint.x == 49.75 * (dungeonSize - 1))
                    {
                        int rand = 0;
                        rand = Random.Range(0, LB.Length);
                        Instantiate(LB[rand], currPoint, Quaternion.identity);
                        //Debug.Log("LB Instantiated");
                    }
                    else
                    {
                        int rand = 0;
                        rand = Random.Range(0, LRB.Length);
                        Instantiate(LRB[rand], currPoint, Quaternion.identity);
                        //Debug.Log("LRB Instantiated");
                    }
                }
                else
                {
                    if (currPoint.x == 0)
                    {
                        int rand = 0;
                        rand = Random.Range(0, TRB.Length);
                        Instantiate(TRB[rand], currPoint, Quaternion.identity);
                        //Debug.Log("TRB Instantiated");
                    }
                    else if (currPoint.x == 49.75 * (dungeonSize - 1))
                    {
                        int rand = 0;
                        rand = Random.Range(0, LTB.Length);
                        Instantiate(LTB[rand], currPoint, Quaternion.identity);
                        //Debug.Log("LTB Instantiated");
                    }
                    else
                    {
                        int rand = 0;
                        rand = Random.Range(0, All4.Length);
                        Instantiate(All4[rand], currPoint, Quaternion.identity);
                        //Debug.Log("All4 Instantiated");
                    }
                }

                if (j != dungeonSize - 1)
                {
                    y += 30;
                }
            }
            y = -30 * (dungeonSize / 2);
            x += 49.75f;
        }
    }
}
