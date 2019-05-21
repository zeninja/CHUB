using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositioner : MonoBehaviour
{
    public int targetWorld;

    public int targetLevel;

    public Vector3 showPos;
    public Vector3 hidePos;

    public AnimationCurve curve;

    void Update()
    {

        if (MetaSlider.GetInstance().InSameWorld(targetWorld))
        {

            if (targetLevel == 0)
            {
                float p = MetaSlider.GetInstance().worldCompletionPct;
                transform.localPosition = Vector3.Lerp(hidePos, showPos, curve.Evaluate(p));
            }
            else
            {
                if (MetaSlider.GetInstance().stageInfo.level == targetLevel)
                {
                    float p = MetaSlider.GetInstance().currentSliderValue;
                    transform.localPosition = Vector3.Lerp(hidePos, showPos, curve.Evaluate(p));

                }
            }
        }
    }
}
