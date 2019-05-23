using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSlider : MonoBehaviour
{

    public SliderState state = SliderState.prep;
    public enum SliderState { prep, active, ending, completed }

    [Range(0, 1)] public float percent;
    Transform start, end, knob;

    public bool devMode = true;
    // public bool isActive;
    bool wasActive;

    public delegate void InstantSliderEvent();
    // public static event InstantSliderEvent OnSliderCompleted;
    public static event InstantSliderEvent OnSliderStarted;
    public static event InstantSliderEvent OnValueChanged;

    public delegate void ContinuousSliderEvent(float p);
    public static event ContinuousSliderEvent OnBackslide;

    // public bool canBackslide = false;

    void Awake()
    {
        start = transform.Find("start");
        end = transform.Find("end");
        knob = transform.Find("knob");
    }

    void Start()
    {

        // box = GetComponent<BoxCollider>();

        hallController = GetComponent<HallController>();

        SetKnobInfo();
        SetKnobToStart();
    }

    void SetKnobInfo()
    {
        // float sliderLength = InfoManager.GetInstance ().realWorld.hallLength;

        float sliderLength = end.position.z - start.position.z;
        float center = start.position.z + sliderLength / 2;

        knob.GetComponent<KnobController>().SetBounds(start.localPosition.z, end.localPosition.z);

        // start.transform.localPosition = new Vector3 (0, 0, -sliderLength / 2);
        // end.transform.localPosition = new Vector3 (0, 0, sliderLength / 2);
        // knob.GetComponent<KnobController> ().bounds = sliderLength / 2;
        // box.size = InfoManager.GetInstance().realWorld.hallDimensions;
    }

    void LateUpdate()
    {
        // if (!devMode && !isActive) { return; }
        // if (!isActive) { return; }

        // if (state == SliderState.active || state == SliderState.ending)
        // {
        GetPercentByKnob();
        // }
    }

    HallController hallController;

    public void SetKnobTarget(Transform target)
    {
        // Debug.Log ("target set");
        knob.GetComponent<KnobController>().target = target;
    }

    void SetKnobToStart()
    {
        knob.transform.position = start.transform.position;
    }

    float maxPercent;
    public float lastPercent;

    void GetPercentByKnob()
    {
        if (!IsActive()) { return; }
        percent = Extensions.mapRange(start.localPosition.z, end.localPosition.z, 0, 1, knob.localPosition.z);

        percent = Mathf.Clamp01(percent);
        // Debug.Log(name + " percent " + percent);

        // nothing has changed
        if (lastPercent == percent) { return; }
        if (percent > lastPercent)
        {
            // forward progress is always allowed
            maxPercent = percent;
            if (OnValueChanged != null)
            {
                OnValueChanged();
            }
        }
        else
        if (percent < lastPercent)
        { // trying to back up
            // no backing up allowed
            if (OnBackslide != null)
            {
                OnBackslide(Mathf.Clamp01(maxPercent - percent));
            }
        }

        lastPercent = percent;
    }

    // public void RoundValue()
    // {
    //     if (percent > .9f)
    //     {
    //         knob.transform.position = end.transform.position;
    //     }
    //     if (percent < .1f)
    //     {
    //         knob.transform.position = start.transform.position;
    //     }
    // }

    bool IsActive()
    {
        return state == SliderState.active;// || state == SliderState.ending;
    }

    void ReleaseTarget()
    {
        knob.GetComponent<KnobController>().target = null;
    }

    void Update()
    {

        ProcessState();
        // SmoothValue ();
    }













    public void HandleStartEntered(Transform target)
    {
        switch (state)
        {
            case SliderState.prep:
                SetKnobTarget(target);
                break;
            case SliderState.active:

                break;
            case SliderState.ending:

                break;
            case SliderState.completed:

                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (state)
        {
            case SliderState.prep:
                // StartCoroutine( MetaSlider.GetInstance().ForceNextSlider());
                // MetaSlider.GetInstance().ForceNextSlider();
                break;
            case SliderState.active:

                break;
            case SliderState.ending:

                break;
            case SliderState.completed:

                break;
        }
    }

    void OnTriggerExit()
    {
        switch (state)
        {
            case SliderState.active:
                // SetState(SliderState.ending);
                if (percent > .8f)
                {
                    SetState(SliderState.completed);
                    // percent = 1;
                    lab.ResetBox();
                }


                // lab.ProcessDilation();
                break;
        }

    }

    // public void HandleExitEntered()
    // {
    //     switch (state)
    //     {
    //         case SliderState.active:
    //             SetState(SliderState.completed);
    //             break;

    //             // case SliderState.prep:

    //             //     break;

    //             // case SliderState.ending:

    //             //     break;
    //             // case SliderState.completed:

    //             //     break;
    //     }
    // }

    public void ProcessState()
    {
        switch (state)
        {
            // case SliderState.prep:

            //     break;
            case SliderState.active:
                // if (MetaSlider.GetInstance().stageInfo.level == 4)
                // {
                // Debug.Log("Slider percent: " + percent);
                // }



                break;

            case SliderState.ending:

                // if (MetaSlider.GetInstance().stageInfo.level == 4)
                // {
                //     Debug.Log("Slider ending: " + percent);
                // }

                // if (percent > .9f)
                // {
                //     RoundValue();
                //     SetState(SliderState.completed);
                // }

                // StartCoroutine(SlideToEnd());
                break;
            case SliderState.completed:
                // percent = 1;
                break;
        }
    }

    public void SetActive()
    {
        percent = 0;
        SetKnobTarget(MetaSlider.GetInstance().playerTarget);
        SetState(SliderState.active);
    }

    public void SetState(SliderState newState)
    {
        state = newState;

        switch (state)
        {
            case SliderState.prep:
                percent = 0;
                ReleaseTarget();
                SetKnobToStart();
                break;

            // case SliderState.ending:
            //     StartCoroutine(SlideToEnd());

            //     break;

            case SliderState.completed:

                percent = 1;

                // lab.EndSlider();

                MetaSlider.GetInstance().HandleSliderCompleted(this);
                SetState(SliderState.prep);
                ReleaseTarget();

                maxPercent = 0;
                percent = 0;
                lastPercent = 0;

                break;
        }
    }

    // IEnumerator SlideToEnd()
    // {
    //     while (percent < 1)
    //     {
    //         percent += Time.deltaTime;
    //         percent = Mathf.Clamp01(percent);

    //         Debug.Log(percent);
    //         lab.ProcessDilation();
    //         yield return new WaitForEndOfFrame();
    //     }

    //     RoundValue();
    //     lab.ProcessDilation();
    //     SetState(SliderState.completed);
    // }

    public LabyrinthController lab;

    // public void ForceSliderEnd()
    // {
    //     SetState(SliderState.completed);
    // }
}