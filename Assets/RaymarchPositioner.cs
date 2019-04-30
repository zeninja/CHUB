using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaymarchPositioner : MonoBehaviour {
    public Vector3 onStart;

    public GiantSlider slider;
    public Vector3 sliderActive;
    public Vector3 sliderInactive;

    // Start is called before the first frame update
    void Start () {
        transform.position = onStart;
    }

    // Update is called once per frame
    void Update () {
        if (slider != null) {
            // Set position based on slider's active state
            transform.position = slider.isActive ? sliderActive : sliderInactive;
        }
    }
}