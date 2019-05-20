using System.Collections;
using System.Collections.Generic;
using RaymarchingToolkit;
using UnityEngine;

public class RayController : MonoBehaviour
{

    // public MetaSlider.StageInfo targetStage;

    public int targetWorld;
    public RaymarchObject outerSphere;
    public Extensions.Property range;
    public AnimationCurve curve;

    void Start()
    {
        GiantSlider.OnValueChanged += ProcessRayObject;
    }

    void ProcessRayObject()
    {
        // if (MetaSlider.GetInstance ().stageInfo != targetStage) { return; }

        // if (targetStage.world == MetaSlider.GetInstance().stageInfo.world &&
        //     targetStage.level == MetaSlider.GetInstance().stageInfo.level)
        // {

        if(MetaSlider.GetInstance().stageInfo.world == targetWorld) {

            float t = MetaSlider.GetInstance().totalCompletionPct;
            float scale = GetScale(range, t);
            outerSphere.GetObjectInput("radius").SetFloat(scale);
        }

    }

    float GetScale(Extensions.Property range, float t)
    {
        float diff = range.end - range.start;
        return  (range.start + diff * curve.Evaluate(t));
    }
}