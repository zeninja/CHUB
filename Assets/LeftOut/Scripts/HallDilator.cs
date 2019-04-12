using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class HallDilator : MonoBehaviour
{
    public List<RaymarchObject> dilatedHalls = new List<RaymarchObject>();
    public RealLabyrinthController realLabryinth;
    public float dilationAmount = 10;


    void LateUpdate()
    {
        if (!realLabryinth.UseDilation()) { return; }

        DilateLength();
        DilateHeight();
        DilateWidth();
    }

    public AnimationCurve animCurve;

    public bool dilateLength;
    public bool dilateHeight;
    public bool dilateWidth;

    public void DilateLength()
    {
        if (!dilateLength) { return; }

        int i = 0;
        foreach (RaymarchObject d in dilatedHalls)
        {
            float startHallLength = realLabryinth.undilatedTotalHallLength;
            float sliderCompletionPct = realLabryinth.hallDilationPct[i];
            float easedSlider = animCurve.Evaluate(sliderCompletionPct);
            float finalHallLength = startHallLength + dilationAmount * easedSlider;

            d.GetObjectInput("z").SetFloat(finalHallLength);
            i++;
        }
    }

    public void DilateHeight()
    {
        if (!dilateHeight) { return; }

        int i = 0;
        foreach (RaymarchObject d in dilatedHalls)
        {
            float startHallHeight = realLabryinth.info_RealWorld.hallHeight;
            float sliderCompletionPct = realLabryinth.hallDilationPct[i];
            float easedSlider = animCurve.Evaluate(sliderCompletionPct);
            float finalHallLength = startHallHeight + dilationAmount * easedSlider;

            d.GetObjectInput("y").SetFloat(finalHallLength);
            i++;
        }
    }

    public void DilateWidth()
    {
        if (!dilateWidth) { return; }

        int i = 0;
        foreach (RaymarchObject d in dilatedHalls)
        {
            float startHallWidth = realLabryinth.info_RealWorld.hallWidth;
            float sliderCompletionPct = realLabryinth.hallDilationPct[i];
            float easedSlider = animCurve.Evaluate(sliderCompletionPct);
            float finalHallLength = startHallWidth + dilationAmount * easedSlider;

            d.GetObjectInput("x").SetFloat(finalHallLength);
            i++;
        }
    }

    public void SetDilation(bool w, bool h, bool l)
    {
        dilateWidth  = w;
        dilateHeight = h;
        dilateLength = l;
    }
}
