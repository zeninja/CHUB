﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSlider : MonoBehaviour {
    BoxCollider box;

    [Range (0, 1)] public float percent;
    Transform start, end, knob;

    public bool devMode = true;
    public bool isActive;

    public delegate void ValueChange ();
    public static event ValueChange OnValueChanged;

    void Start () {
        start = transform.Find ("start");
        end = transform.Find ("end");
        knob = transform.Find ("knob");
        box = GetComponent<BoxCollider> ();

        hallController = GetComponent<HallController> ();

        SetKnobInfo ();
        ResetToStart ();
    }

    void SetKnobInfo () {
        float sliderLength = InfoManager.GetInstance ().realWorld.hallLength;

        start.transform.localPosition = new Vector3 (0, 0, -sliderLength / 2);
        end.transform.localPosition = new Vector3 (0, 0, sliderLength / 2);
        knob.GetComponent<KnobController> ().bounds = sliderLength / 2;
        // box.size = InfoManager.GetInstance().realWorld.hallDimensions;
    }

    void LateUpdate () {
        if (!devMode && !isActive) { return; }
        GetPercentByKnob ();
    }

    public void SetKnobTarget (Transform target) {
        knob.GetComponent<KnobController> ().target = target;
    }

    public void TryReleaseTarget () {
        // only release the target if the player goes into a new hallway, not back into the current hallway (as indicated by isActive)
        if (isActive) { return; }
        knob.GetComponent<KnobController> ().target = null;
    }

    HallController hallController;

    void OnTriggerEnter (Collider other) {
        Debug.Log ("HIT");
        if (other.CompareTag ("Player")) {
            HandleSliderEntered (other.transform);
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            HandleSliderExited ();
        }
    }

    public void ResetIfNotActive () {
        if (!isActive) {
            ResetToStart ();
        }
    }

    void ResetToStart () {
        knob.transform.position = start.transform.position;
    }

    float lastPercent;

    void GetPercentByKnob () {
        percent = Extensions.mapRange (start.localPosition.z, end.localPosition.z, 0, 1, knob.localPosition.z);

        if (lastPercent != percent) {
            // EventManager.TriggerEvent ("OnValueChanged");

            if (OnValueChanged != null) {
                OnValueChanged ();
            }
        }

        lastPercent = percent;
    }

    public void RoundValue () {
        if (percent >.9f) {
            knob.transform.position = end.transform.position;
        }
        if (percent < .1f) {
            knob.transform.position = start.transform.position;
        }
    }

    // ------------------------------------------

    // Start corner
    public void HandleStartEntered () {
        ResetIfNotActive ();

        AudioManager.instance.HandleSliderEntered ();
    }

    public void HandleStartExited () {

    }

    // Slider
    public void HandleSliderEntered (Transform target) {

        hallController.SetDilationType ();
        SetKnobTarget (target);
        isActive = true;

    }

    public void HandleSliderExited () {
        TryReleaseTarget ();
        RoundValue ();

    }

    // End Corner
    public void HandleExitEntered () {
        if (isActive) {
            AudioManager.instance.Play ("HallCompleted");
        }

        isActive = false;
        ResetIfNotActive ();
    }

    public void HandleExitExited () {

    }
}