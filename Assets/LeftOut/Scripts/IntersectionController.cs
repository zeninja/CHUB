using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

// [ExecuteInEditMode]
public class IntersectionController : MonoBehaviour
{
    public RaymarchBlend blend;
    public RaymarchObject intersector;
    public Transform followTarget;

    public float radius = 10;
    float startRadius;

    void Start() {
        startRadius = radius;
    }

    void Update() {
        intersector.shape.inputs[0].SetFloat(radius);

        FluctuateSize();
        FollowTarget();
    }

    public bool fluctuateSize;
    public float fluxAmt = 3;

    void FluctuateSize() {
        if (fluctuateSize) {
            radius = startRadius + fluxAmt * Mathf.Sin(Time.time);
        }
    }

    void FollowTarget() {
        if (followTarget != null) {
            intersector.transform.position = followTarget.position;
        }
    }
}
