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
    public VoidLabyrinthController labyrinth;
    public float dilationAmount = 10;

    void Start() {
        DilateHalls();
    }

    public void DilateHalls()
    {
        DilateLength();
        DilateHeight();
        DilateWidth();

        AdjustPositions();
    }



    public AnimationCurve animCurve;

    bool dilateLength;
    bool dilateHeight;
    bool dilateWidth;

    [SerializeField]
    public Vector3[] dilatedDimensions = new Vector3[4];


    public void DilateLength()
    {
        if (!dilateLength) { return; }
        Debug.Log("dialting");

        int i = 0;
        foreach (RaymarchObject d in dilatedHalls)
        {
            float startHallLength = InfoManager.GetInstance().voidWorld.hallLength;
            float sliderCompletionPct = labyrinth.hallDilationPct[i];
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
            float startHallHeight = InfoManager.GetInstance().voidWorld.hallHeight;
            float sliderCompletionPct = labyrinth.hallDilationPct[i];
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
            float startHallWidth = InfoManager.GetInstance().voidWorld.hallWidth;
            float sliderCompletionPct = labyrinth.hallDilationPct[i];
            float easedSlider = animCurve.Evaluate(sliderCompletionPct);

            float dilatedWidth = startHallWidth + dilationAmount * easedSlider;
            dilatedDimensions[i] = new Vector3(dilatedWidth, dilatedDimensions[i].y, dilatedDimensions[i].z);

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


    void AdjustPositions()
    {
        int i = 0;
        foreach (RaymarchObject d in dilatedHalls)
        {
            Vector3 orthDir    = InfoManager.GetInstance().orthographicPts[i].normalized;
            float distTowall   = InfoManager.GetInstance().realWorld.distanceToInnerWall;
            float dilatedWidth = dilatedDimensions[i].x;

            Vector3 adjustedPos = orthDir * (distTowall + dilatedWidth);

            d.transform.localPosition = new Vector3(adjustedPos.x, 0, adjustedPos.z);
            i++;
        }
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < 4; i++)
        {

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(InfoManager.GetInstance().orthographicPts[i].normalized * (InfoManager.GetInstance().realWorld.distanceToInnerWall + dilatedDimensions[i].x), .125f);
        }
    }
}
