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

    public static Vector3[] dilatedDimensions = new Vector3[4];

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
            dilatedDimensions[i] = new Vector3(dilatedDimensions[i].x, dilatedDimensions[i].y, dilatedLength);


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
            dilatedDimensions[i] = new Vector3(dilatedDimensions[i].x, dilatedHeight, dilatedDimensions[i].z);


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

            dilatedDimensions[i] = new Vector3(dilatedWidth, dilatedDimensions[i].y, dilatedDimensions[i].z);

            // Debug.Log(dilatedWidth + "; " + finalWidth);

            d.GetObjectInput("x").SetFloat(dilatedWidth);
            i++;
        }
    }

    public Vector3 GetDilatedDimensions(int i) {
        return dilatedDimensions[i];
    }

    public void SetDilation(bool w, bool h, bool l)
    {
        dilateWidth = w;
        dilateHeight = h;
        dilateLength = l;
    }
}
