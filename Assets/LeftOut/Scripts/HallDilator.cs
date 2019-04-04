using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class HallDilator : MonoBehaviour
{

    public List<RaymarchObject> dilatedHalls = new List<RaymarchObject>();

    float finalHallLength;

    public RealLabyrinthController realLabryinth;

    public float dilationAmount = 10;


    void Update()
    {
        // UpdateHallDilation();
    }

    public AnimationCurve animCurve;

    public void UpdateHallDilation() {
        
        int i = 0;
        foreach (RaymarchObject d in dilatedHalls)
        {
            float startHallLength     = realLabryinth.maxHallLength;
            float sliderCompletionPct = realLabryinth.hallDilationPct[i];
            float easedSlider         = animCurve.Evaluate(sliderCompletionPct);
            float finalHallLength     = startHallLength + dilationAmount * easedSlider;

            d.GetObjectInput("z").SetFloat(finalHallLength);
            i++;
        }
    }
}
