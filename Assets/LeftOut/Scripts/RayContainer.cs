using System.Collections;
using System.Collections.Generic;
using RaymarchingToolkit;
using UnityEngine;

public class RayContainer : MonoBehaviour {

    public MetaSlider.StageInfo targetStage;
    public RaymarchObject outerSphere;
    public Extensions.Property range;
    public AnimationCurve curve;

    void Start () {
        GiantSlider.OnValueChanged += ProcessRayObject;
    }

    void ProcessRayObject () {
        if (MetaSlider.GetInstance ().stageInfo != targetStage) { return; }

        float t = MetaSlider.GetInstance ().currentSliderValue;
        Vector3 scale = GetScale (range, t);
        outerSphere.GetObjectInput ("scale").SetVector3 (scale);

    }

    Vector3 GetScale (Extensions.Property range, float t) {
        float diff = range.end - range.start;
        return Vector3.one * (range.start + diff * curve.Evaluate(t));
    }
}