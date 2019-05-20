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
    bool wasActive;

    public delegate void InstantSliderEvent ();
    // public static event InstantSliderEvent OnSliderCompleted;
    public static event InstantSliderEvent OnSliderStarted;
    public static event InstantSliderEvent OnValueChanged;

    public delegate void ContinuousSliderEvent (float p);
    public static event ContinuousSliderEvent OnBackslide;

    public bool canBackslide = false;

    void Start () {
        start = transform.Find ("start");
        end = transform.Find ("end");
        knob = transform.Find ("knob");
        // box = GetComponent<BoxCollider>();

        hallController = GetComponent<HallController> ();

        SetKnobInfo ();
        SetKnobToStart ();
    }

    void SetKnobInfo () {
        float sliderLength = InfoManager.GetInstance ().realWorld.hallLength;

        // start.transform.localPosition = new Vector3 (0, 0, -sliderLength / 2);
        // end.transform.localPosition = new Vector3 (0, 0, sliderLength / 2);
        knob.GetComponent<KnobController> ().bounds = sliderLength / 2;
        // box.size = InfoManager.GetInstance().realWorld.hallDimensions;
    }

    void LateUpdate () {
        if (!devMode && !isActive) { return; }
        GetPercentByKnob ();
    }

    HallController hallController;

    public void SetKnobTarget (Transform target) {
        knob.GetComponent<KnobController> ().target = target;
    }

    void SetKnobToStart () {
        knob.transform.position = start.transform.position;
    }

    // NOT THE CLEANEST BUT IT WORKS // 
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

    public void StartCornerStay (Transform player) {
        if (isActive) {
            SetKnobTarget (player);

            if (!wasActive) {
                if (OnSliderStarted != null) {
                    // Debug.Log("Starting slider. Current info: " + MetaSlider.GetInstance().stageInfo.world + "-" + MetaSlider.GetInstance().stageInfo.level);
                    OnSliderStarted ();
                }
                wasActive = true;
            }

        } else {
        }
    }

    public void EndCornerStay () {
        // Debug.Log("END CORNER STAYING");

        if (isActive) {
            if (percent > .95f) {
                // Debug.Log ("Going to next slider");
                // Go to next slider
                ReleaseTarget ();

                // if (OnSliderCompleted != null) {
                //     Debug.Log("Slider completed");
                //     OnSliderCompleted ();
                // }
                RoundValue();

                wasActive = false;
                MetaSlider.GetInstance ().HandleSliderCompleted (this);
            }
        }
    }

    // public void ResetIfNotActive () {
    //     if (!isActive) {
    //         ResetToStart ();
    //     }
    // }

    // Start corner
    // public void HandleStartEntered () {
    //     if (OnSliderStarted != null) {
    //         OnSliderStarted ();
    //     }

    //     // ResetIfNotActive ();
    // }

    // public void HandleStartExited () {

    // }

    // Slider
    // public void HandleSliderEntered (Transform target) {
    //     hallController.SetDilationType ();
    //     SetKnobTarget (target);
    //     // isActive = true;
    // }

    // public void HandleSliderExited () {
    //     // ReleaseTarget ();
    //     // RoundValue ();

    //     // if (isActive) {
    //     //     // AudioManager.instance.Play ("HallCompleted");
    //     //     MetaSlider.GetInstance ().HandleSliderCompleted (this);

    //     //     // if (OnSliderCompleted != null) {
    //     //     //     OnSliderCompleted ();
    //     //     // }
    //     // }

    //     // Debug.Break();

    //     RoundValue();
    //     Debug.Log(" ---- Slider exited ---- ");
    // }

    // End Corner
    // public void HandleExitEntered () {
    //     // if (isActive) {   
    //     //     // AudioManager.instance.Play ("HallCompleted");
    //     //     MetaSlider.GetInstance ().HandleSliderCompleted (this);
    //     // }

    //     // ResetIfNotActive ();

    //     // if (OnSliderCompleted != null) {
    //     //     OnSliderCompleted();
    //     // }
    // }

    // public void HandleExitExited () {

    // }
}