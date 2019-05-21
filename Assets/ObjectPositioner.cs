using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositioner : MonoBehaviour
{
    // public int targetWorld;

    // public int targetLevel;

    public MetaSlider.StageInfo targetStage;

    public Vector3 showPos;
    public Vector3 hidePos;

    public AnimationCurve curve;

    void Update()
    {

        if (MetaSlider.GetInstance().StageInfoMatches(targetStage))
        {
            float p = MetaSlider.GetInstance().currentSliderValue;
            transform.localPosition = Vector3.Lerp(hidePos, showPos, curve.Evaluate(p));
        }


        // if (MetaSlider.GetInstance().InSameWorld(targetWorld))
        // {

        //     // Vector3 diff = showPos - hidePos;

        //     // if (targetLevel == 0)
        //     // {
        //     //     float p = MetaSlider.GetInstance().worldCompletionPct;
        //     //     transform.localPosition = Vector3.Lerp(hidePos, showPos, curve.Evaluate(p));
        //     // }
        //     // else
        //     // {


        //     if (MetaSlider.GetInstance().)
        //     {
        //         float p = MetaSlider.GetInstance().currentSliderValue;
        //         transform.localPosition = Vector3.Lerp(hidePos, showPos, curve.Evaluate(p));
        //     }


        //     // }
        // }
    }
}
