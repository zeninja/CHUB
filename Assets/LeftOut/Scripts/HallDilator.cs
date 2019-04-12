using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class HallDilator : MonoBehaviour
{

    private static HallDilator instance;
    public static HallDilator GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public List<RaymarchObject> dilatedHalls = new List<RaymarchObject>();
    public RealLabyrinthController realLabryinth;
    public float dilationAmount = 10;


    void LateUpdate()
    {
        if (!realLabryinth.UseDilation()) { return; }

        DilateLength();
        DilateHeight();
        DilateWidth();

        // Debug.Log("dialted width " + dilatedWidth);
    }

    public AnimationCurve animCurve;

    bool dilateLength;
    bool dilateHeight;
    bool dilateWidth;

    static float finalWidth;
    static float finalHeight;
    static float finalLength;

    public static float GetDilatedWidth() { return finalWidth; }
    public static float GetDilatedHeight() { return finalHeight; }
    public static float GetDilatedLength() { return finalLength; }

    public void DilateLength()
    {
        if (!dilateLength) { return; }

        int i = 0;
        foreach (RaymarchObject d in dilatedHalls)
        {
            float startHallLength = realLabryinth.info_RealWorld.totalHallLength;
            float sliderCompletionPct = realLabryinth.hallDilationPct[i];
            float easedSlider = animCurve.Evaluate(sliderCompletionPct);

            float dilatedLength = startHallLength + dilationAmount * easedSlider;

            if (sliderCompletionPct != 0) 
            {
                finalLength = dilatedLength;
            }

            d.GetObjectInput("z").SetFloat(dilatedLength);
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

            float dilatedHeight = startHallHeight + dilationAmount * easedSlider;

            if (sliderCompletionPct != 0) 
            {
                finalHeight = dilatedHeight;
            }

            d.GetObjectInput("y").SetFloat(dilatedHeight);
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

            float dilatedWidth = startHallWidth + dilationAmount * easedSlider;

            if (sliderCompletionPct != 0) 
            {
                finalWidth = dilatedWidth;
            }

            Debug.Log(dilatedWidth + "; " + finalWidth);

            d.GetObjectInput("x").SetFloat(dilatedWidth);
            i++;
        }
    }

    public void SetDilation(bool w, bool h, bool l)
    {
        dilateWidth = w;
        dilateHeight = h;
        dilateLength = l;
    }
}
