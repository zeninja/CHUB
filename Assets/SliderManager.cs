using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    GiantSlider[] sliders;
    public static float sliderLength = 2;

    void Start()
    {
        sliders = GetComponentsInChildren<GiantSlider>();
    }

    void Update() {
        sliderLength = InfoManager.GetInstance().realWorld.hallLength;
    }
}
