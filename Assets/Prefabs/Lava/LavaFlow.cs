using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFlow : MonoBehaviour
{
    public float rate = 1f;
    public Vector2 direction;
    private MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Material mat = mr.material;

        Vector2 displacement = direction * (rate * Time.deltaTime);
        mat.mainTextureOffset += displacement;
    }
}
