using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSlider : MonoBehaviour {

    public SliderState state = SliderState.prep;
    public enum SliderState { prep, active, completed }

    // BoxCollider box;

    [Range (0, 1)] public float percent;
    Transform start, end, knob;

    public bool devMode = true;
    public bool isActive;

    public delegate void InstantSliderEvent ();
    public static event  InstantSliderEvent OnSliderCompleted;
    public static event  InstantSliderEvent OnSliderStarted;

    public delegate void ContinuousSliderEvent (float p);
    public static event  InstantSliderEvent OnValueChanged;
    public static event  ContinuousSliderEvent OnBackslide;

    public bool canBackslide = false;

    void Start () {
        start = transform.Find ("start");
        end = transform.Find ("end");
        knob = transform.Find ("knob");
        // box = GetComponent<BoxCollider>();

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

    HallController hallController;

    void OnTriggerEnter (Collider other) {
        // Debug.Log ("Slider entered");
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
        Debug.Log("Resetting slider to start");
        knob.transform.position = start.transform.position;
    }

    float maxPercent;
    float lastPercent;

    void GetPercentByKnob () {
        percent = Extensions.mapRange (start.localPosition.z, end.localPosition.z, 0, 1, knob.localPosition.z);

        // nothing has changed
        if (lastPercent == percent) { return; }

        if (percent > lastPercent) {
            // forward progress is always allowed
            maxPercent = percent;
            if (OnValueChanged != null) {
                OnValueChanged ();
            }
        } else // trying to back up
            if (percent < lastPercent) {

                if (canBackslide) {

                    // backing up allowed
                    if (OnValueChanged != null) {
                        OnValueChanged ();
                    }
                } else {

                    // no backing up allowed
                    if (OnBackslide != null) {
                        OnBackslide (maxPercent - percent);
                    }
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

    void ReleaseTarget () {
        knob.GetComponent<KnobController> ().target = null;
    }

    // ------------------------------------------

    // Start corner
    public void HandleStartEntered () {
        if (OnSliderStarted != null) {
            OnSliderStarted();
        }

        ResetIfNotActive ();

    }

    public void HandleStartExited () {

    }

    // Slider
    public void HandleSliderEntered (Transform target) {
        hallController.SetDilationType ();
        SetKnobTarget (target);
        // isActive = true;
    }

    public void HandleSliderExited () {
        // TryReleaseTarget ();
        ReleaseTarget ();
        RoundValue ();

        if (OnSliderCompleted != null) {
            OnSliderCompleted();
        }

        if (isActive) {   
            // AudioManager.instance.Play ("HallCompleted");
            MetaSlider.GetInstance ().HandleSliderCompleted (this);
        }
    }

    // End Corner
    public void HandleExitEntered () {
        // if (isActive) {   
        //     // AudioManager.instance.Play ("HallCompleted");
        //     MetaSlider.GetInstance ().HandleSliderCompleted (this);
        // }

        // ResetIfNotActive ();

        // if (OnSliderCompleted != null) {
        //     OnSliderCompleted();
        // }
    }

    public void HandleExitExited() {

    }
}