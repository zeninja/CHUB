using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;


public class VoidLabyrinthController : MonoBehaviour
{
    public List<RaymarchObject> voidHalls = new List<RaymarchObject>();
    // public RaymarchObject ground;

    public HallDilator hallDilator;

    void Start()
    {
        SetVoidHallDimensions();
    }

    void Update()
    {
        GetHallDilationFromSliders();
        hallDilator.DilateHalls();
    }

    void SetVoidHallDimensions()
    {
        for (int i = 0; i < 4; i++)
        {
            voidHalls[i].GetObjectInput("x").SetFloat(InfoManager.GetInstance().voidWorld.hallWidth);
            voidHalls[i].GetObjectInput("y").SetFloat(InfoManager.GetInstance().voidWorld.hallHeight);
            voidHalls[i].GetObjectInput("z").SetFloat(InfoManager.GetInstance().voidWorld.hallLength);
        }
    }

    // void SetVoidHallPositions()
    // {
    //     for (int i = 0; i < 4; i++)
    //     {
    //         voidHalls[i].transform.position = InfoManager.GetInstance().orthographicPts[i] * InfoManager.GetInstance().voidWorld.distToHall_frwd;
    //     }
    // }

    [Range(0, 1f)]
    public List<float> hallDilationPct = new List<float>(4);
    public List<GiantSlider> sliders = new List<GiantSlider>(4);
    // public bool dilateHalls;

    void GetHallDilationFromSliders()
    {
        for (int i = 0; i < hallDilationPct.Count; i++)
        {
            hallDilationPct[i] = sliders[i].percent;
        }
    }

    // public bool UseDilation()
    // {
    //     return dilateHalls;
    // }

    // void OnDrawGizmos()
    // {
    //     for (int i = 0; i < 4; i++)
    //     {
    //         // Gizmos.color = Color.yellow;
    //         // Gizmos.DrawWireSphere(adjustedPoints[i], .5f);
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawWireSphere(realLabyrinth.orthographicPts[i].normalized, .25f);
    //     }
    // }
}
