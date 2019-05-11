using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolutionController : MonoBehaviour
{
    private RevolutionController instance;
    public  RevolutionController GetInstance() {
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

    int world = 1;
    int level = 1;

    string currentStage;

    int lastTriggeredSlider;

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
            level = 0;
        }

        SetCurrentStage();
        Debug.Log("Slider completed. Current stage: " + currentStage);
        lastTriggeredSlider = sliderIndex;
    }

    int GetSliderIndex(GiantSlider slider) {
        for(int i = 0; i < 4; i++) {
            if (slider == SliderInfo.GetInstance().sliders[i]) {
                return i;
            }
        }
        Debug.LogError("Slider index not found");
        return -1;
    }

    void SetCurrentStage() {
        currentStage = world + "-" + level;
    }
}
