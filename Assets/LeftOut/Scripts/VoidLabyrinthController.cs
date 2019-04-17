 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;


public class VoidLabyrinthController : MonoBehaviour
{
    public HallDilator hallDilator;
    public List<RaymarchObject> voidHalls = new List<RaymarchObject>();

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
            
            float x = InfoManager.GetInstance().voidWorld.hallWidth;
            float y = InfoManager.GetInstance().voidWorld.hallHeight;
            float z = InfoManager.GetInstance().voidWorld.hallLength;

            // rounded hall attempt (the roundness doesn't play nice)
            // voidHalls[i].GetObjectInput("size").SetVector3(new Vector3(x, y, z));

            // regular halls
            voidHalls[i].GetObjectInput("x").SetFloat(x);
            voidHalls[i].GetObjectInput("y").SetFloat(y);
            voidHalls[i].GetObjectInput("z").SetFloat(z);
        }
    }

    [Range(0, 1f)]
    public List<float> hallDilationPct = new List<float>(4);
    public List<GiantSlider> sliders = new List<GiantSlider>(4);

    void GetHallDilationFromSliders()
    {
        for (int i = 0; i < hallDilationPct.Count; i++)
        {
            hallDilationPct[i] = sliders[i].percent;
        }
    }

    public bool syncHalls = false;

    public float GetDilationPct(int i)
    {
        if (syncHalls)
        {
            return Mathf.Max(hallDilationPct.ToArray());
        }
        else
        {
            return hallDilationPct[i];
        }
    }
}
