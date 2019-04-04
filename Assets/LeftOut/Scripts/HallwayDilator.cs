using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

[ExecuteInEditMode]
public class HallwayDilator : MonoBehaviour
{
    [Range(0f, 1)]
    public float dilationPercent;
    public float dilationAmount;

    public RaymarchObject reference;
    public RaymarchObject processed;


    void Update() {

        float referenceSize = reference.GetObjectInput("z").floatValue;
        processed.GetObjectInput("z").SetFloat(referenceSize + dilationPercent * dilationAmount);
    }

}
