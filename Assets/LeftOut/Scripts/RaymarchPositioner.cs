using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaymarchPositioner : MonoBehaviour {

    public int targetWorld;
    public Vector3 showPos;
    public Vector3 hidePos;
    void Update () {
        transform.localPosition = targetWorld == MetaSlider.GetInstance ().stageInfo.world ? showPos : hidePos;
    }
}