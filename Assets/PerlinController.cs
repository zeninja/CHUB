using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinController : MonoBehaviour
{

    public float test;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        y = Random.Range(0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        test = Mathf.PerlinNoise(Time.time, y);
    }
}
