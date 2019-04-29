using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaymarchPositioner : MonoBehaviour
{
    public Vector3 onStart;
    // public Vector3 inRevolution;
    // public int targetRevolution;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = onStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
