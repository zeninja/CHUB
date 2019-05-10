using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolutionController : MonoBehaviour
{
    private RevolutionController instance;
    public RevolutionController GetInstance() {
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

    int world;
    int level;

    public void HandleSliderCompleted(GiantSlider slider)
    {
        int levelCompletionIndex = -1;

        for (int i = 0; i < 4; i++) {
            if (slider == SliderInfo.GetInstance().sliders[i]) {
                levelCompletionIndex = i;
            }
        }

        if (levelCompletionIndex != -1) {
            
        }
    }
}
