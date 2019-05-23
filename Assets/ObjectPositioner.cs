﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositioner : MonoBehaviour {

    public int world;

    // Vector3 showPos;
    // Vector3 hidePos;
    public AnimationCurve curve;


    // public Vector3 hidePos = new Vector3(0, 100, 0);

    void Update () {
        float p = 0;
        float yVal = 0;

        if (MetaSlider.GetInstance ().stageInfo.world ==  world) {
            p = MetaSlider.GetInstance ().worldCompletionPct;
            yVal = curve.Evaluate (p);
        }
        else {
            if(MetaSlider.GetInstance().stageInfo.world > world) {
                yVal = 100;
            }
        }


        Vector3 pos = transform.localPosition;
        pos.y = yVal;
        transform.localPosition = pos;
    }
}