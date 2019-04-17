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
    List<Vector3> startPositions = new List<Vector3>();
    public RaymarchObject labyrinthMarble;
    public VoidLabyrinthController labyrinth;
    public float dilationAmount = 10;

    void Start()
    {
        DilateHalls();
    }

    public void DilateHalls()
    {
        DilateLength();
        DilateHeight();
        DilateWidth();

        // AdjustPositions();
    }



    public AnimationCurve animCurve;

    public bool dilateLength;
    public bool dilateHeight;
    public bool dilateWidth;

    [SerializeField]
    public Vector3[] dilatedDimensions = new Vector3[4];


    public void DilateLength()
    {
        if (!dilateLength) { return; }

        int i = 0;
        foreach (RaymarchObject d in dilatedHalls)
        {
            float startHallLength = InfoManager.GetInstance().voidWorld.hallLength;
            float dilationPct = labyrinth.GetDilationPct(i);
            float easedSlider = animCurve.Evaluate(dilationPct);

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
            float startHallHeight = InfoManager.GetInstance().voidWorld.hallHeight;
            float sliderCompletionPct = labyrinth.GetDilationPct(i);
            float easedSlider = animCurve.Evaluate(sliderCompletionPct);

            float dilatedHeight = startHallHeight + dilationAmount * easedSlider;
            dilatedDimensions[i] = new Vector3(dilatedDimensions[i].x, dilatedHeight, dilatedDimensions[i].z);

            // Adjust the positions to compensate for the height offset
            // Vector3 tmep = d.transform.localPosition;
            // Vector3 add  = AdjustHeightPos(i);
            // d.transform.localPosition = tmep + add;


            d.GetObjectInput("y").SetFloat(dilatedHeight);
            i++;
        }

        labyrinthMarble.GetObjectInput("y").SetFloat(dilatedDimensions[0].y - 1);
    }

    public void DilateWidth()
    {
        if (!dilateWidth) { return; }

        int i = 0;
        foreach (RaymarchObject d in dilatedHalls)
        {
            float startHallWidth = InfoManager.GetInstance().voidWorld.hallWidth;
            float sliderCompletionPct = labyrinth.GetDilationPct(i);
            float easedSlider = animCurve.Evaluate(sliderCompletionPct);

            float dilatedWidth = startHallWidth + dilationAmount * easedSlider;
            dilatedDimensions[i] = new Vector3(dilatedWidth, dilatedDimensions[i].y, dilatedDimensions[i].z);

            // Adjust the positions to compensate for the width offset
            d.transform.localPosition = AdjustWidthPos(i);

            d.GetObjectInput("x").SetFloat(dilatedWidth);
            i++;

        }
    }

    Vector3 AdjustWidthPos(int i)
    {
        Vector3 orthDir = InfoManager.GetInstance().orthographicPts[i].normalized;
        float distTowall = InfoManager.GetInstance().realWorld.distanceToInnerWall;
        float dilatedWidth = dilatedDimensions[i].x;
        float dilatedLength = dilatedDimensions[i].z;

        Vector3 adjustedPos = orthDir * (distTowall + dilatedWidth);
        return new Vector3(adjustedPos.x, 0, adjustedPos.z);
    }

    Vector3 AdjustHeightPos(int i)
    {
        float dilatehdHeight = dilatedDimensions[i].y;

        Vector3 adjustedPos = new Vector3(0, dilatehdHeight / 2, 0);
        return  adjustedPos;
    }

    public void SetDilation(bool w, bool h, bool l)
    {
        dilateWidth  = w;
        dilateHeight = h;
        dilateLength = l;
    }


    // void AdjustPositions()
    // {
    //     int i = 0;
    //     foreach (RaymarchObject d in dilatedHalls)
    //     {
    //         Vector3 orthDir = InfoManager.GetInstance().orthographicPts[i].normalized;
    //         float distTowall = InfoManager.GetInstance().realWorld.distanceToInnerWall;
    //         float dilatedWidth = dilatedDimensions[i].x;
    //         float dilatedLength = dilatedDimensions[i].z;
    //         i++;

    //         // if (dilateLength)
    //         // {
    //         //     Vector3 adjustedPos = orthDir * (distTowall + dilatedWidth);
    //         //     d.transform.localPosition = new Vector3(adjustedPos.x, 0, adjustedPos.z);
    //         // }

    //         if (dilatedWidth == 0) { return; }
    //         if (dilateWidth)
    //         {
    //             Vector3 adjustedPos = orthDir * (distTowall + dilatedWidth);
    //             d.transform.localPosition = new Vector3(adjustedPos.x, 0, adjustedPos.z);
    //         }
    //     }
    // }

    void OnDrawGizmos()
    {
        for (int i = 0; i < 4; i++)
        {
            if (InfoManager.GetInstance().orthographicPts.Count == 0) { return; }
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(InfoManager.GetInstance().orthographicPts[i].normalized * (InfoManager.GetInstance().realWorld.distanceToInnerWall + dilatedDimensions[i].x), .125f);
        }
    }
}
