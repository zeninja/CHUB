using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

[ExecuteInEditMode]
public class IntersectionController : MonoBehaviour
{
    public RaymarchObject intersectionObject;
    public bool useIntersection = false;
    public RaymarchBlend blend;
    public float radius = 10;

    void Update() {
        intersectionObject.shape.inputs[0].SetFloat(radius);

        FluctuateSize();
    }

    public bool fluctuate;

    void FluctuateSize() {
        if (fluctuate) {
            
        }
    }
}
