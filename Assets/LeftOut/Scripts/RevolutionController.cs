﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolutionController : MonoBehaviour
{
    private static RevolutionController instance;
    public static RevolutionController GetInstance() {
        return instance;
    }

    void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            if(instance != this) {
                Destroy(this.gameObject);
            }
        }
    }

    public static int world = 1;
    public static int level = 1;

    public static string currentStage;

    int lastTriggeredSlider = -1;

    void Start() {
        SetCurrentStage();
    }

    public void HandleSliderCompleted(GiantSlider slider)
    {

        int sliderIndex = GetSliderIndex(slider);
        if (sliderIndex > lastTriggeredSlider) {
            level++;
        } 

        if(level > 4) {
            world++;
            level = 1;
            lastTriggeredSlider = 0;
        }

        SetCurrentStage();
        Debug.Log("Slider completed. Current stage: " + currentStage);
        lastTriggeredSlider = sliderIndex;
    }

    int GetSliderIndex(GiantSlider slider) {
        for(int i = 0; i < 4; i++) {
            if (slider == MetaSlider.GetInstance().sliders[i]) {
                return i;
            }
        }
        Debug.LogError("Slider index not found");
        return -1;
    }

    void SetCurrentStage() {
        currentStage = world + "-" + level;
    }

    void OnGUI() {
        GUI.Label(new Rect(0, 0, 100, 100), currentStage);
    }
}
