using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositioner : MonoBehaviour {

    public int world;

    // Vector3 showPos;
    // Vector3 hidePos;
    public AnimationCurve curve;


    void Update () {
        float p = 0;

        if (MetaSlider.GetInstance ().stageInfo.world ==  world) {
            p = MetaSlider.GetInstance ().currentSliderValue;
        }

        float yVal = curve.Evaluate (p);

        Vector3 pos = transform.localPosition;
        pos.y = yVal;
        transform.localPosition = pos;
    }
}