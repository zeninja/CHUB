using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class HallSegment : MonoBehaviour
{
    RaymarchObject hall;
    RaymarchObject corner;

    GiantSlider slider;

    void Awake() {
        hall = transform.Find("Hall").GetComponent<RaymarchObject>();
        corner = transform.Find("Corner").GetComponent<RaymarchObject>();
        slider = transform.Find("GiantSlider").GetComponent<GiantSlider>();
    }

    public void SetHallinfo(Vector3 info)
    {
        hall.GetObjectInput("x").SetFloat(info.x);
        hall.GetObjectInput("y").SetFloat(info.y);
        hall.GetObjectInput("z").SetFloat(info.z);
    }

    public void SetCornerInfo(Vector3 info)
    {
        corner.GetObjectInput("x").SetFloat(info.x);
        corner.GetObjectInput("y").SetFloat(info.y);
        corner.GetObjectInput("z").SetFloat(info.z);
    }

    public void SetSliderInfo() {
        
    }
}
