using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class MarblePositioner : MonoBehaviour
{
    RaymarchObject marble;

    void Start() {
        marble = GetComponent<RaymarchObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempPos = transform.position;
        tempPos.y = marble.GetObjectInput("y").floatValue;
        transform.position = tempPos;
    }
}
