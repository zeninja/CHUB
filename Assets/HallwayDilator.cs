using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class HallwayDilator : MonoBehaviour
{
    public RaymarchObject refObj;
    public RaymarchObject dilObj;

    [Range(0f, 1f)]
    public float dilationPct;
    public float dilationAmt = 10f;

    void Update() {
        // Debug.Log((dilObj.GetObjectInput("z").);

        // dilObj.GetObjectInput("z").SetFloat(CrefObj.GetObjectInput("z") + dilationAmt * dilationPct);
    }
}
