using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSlider : MonoBehaviour
{
    BoxCollider box;

    [Range(0, 1)] public float percent;
    Transform start, end, knob;

    public bool devMode = true;
    public bool isActive;

    public enum SliderType { length, width, height, lengthAndWidth, lengthAndHeight, widthAndHeight };
    public SliderType sliderType = SliderType.length;



    void Start()
    {
        start = transform.Find("start");
        end = transform.Find("end");
        knob = transform.Find("knob");
        box = GetComponent<BoxCollider>();

        // SetColliderInfo();

        SetKnobInfo();
        ResetToStart();
    }

    void SetKnobInfo()
    {
        start.transform.localPosition = new Vector3(0, 0, -SliderManager.sliderLength / 2);
        end.transform.localPosition   = new Vector3(0, 0,  SliderManager.sliderLength / 2);
        knob.GetComponent<KnobController>().bounds       = SliderManager.sliderLength / 2 ;
        // box.size = InfoManager.GetInstance().realWorld.hallDimensions;
    }

    void LateUpdate()
    {
        if (!devMode && !isActive) { return; }
        GetPercentByKnob();
    }

    public void SetKnobTarget(Transform target)
    {
        isActive = true;
        knob.GetComponent<KnobController>().target = target;
    }

    public void ReleaseTarget()
    {
        knob.GetComponent<KnobController>().target = null;
        isActive = false;
    }

    void ResetToStart()
    {
        // Debug.Log("Value reset");
        knob.transform.position = start.transform.position;
    }

    void GetPercentByKnob()
    {
        percent = Extensions.mapRange(start.localPosition.z, end.localPosition.z, 0, 1, knob.localPosition.z);
    }

    public void SetDilationType()
    {
        // if(isActive) { return; }    // only change type if not currently being used

        switch (sliderType)
        {
            // width, height, length
            case SliderType.length:
                HallDilator.GetInstance().SetDilation(false, false, true);
                break;
            case SliderType.width:
                HallDilator.GetInstance().SetDilation(true, false, false);
                break;
            case SliderType.height:
                HallDilator.GetInstance().SetDilation(false, true, false);
                break;
            case SliderType.lengthAndWidth:
                HallDilator.GetInstance().SetDilation(true, false, true);
                break;
            case SliderType.lengthAndHeight:
                HallDilator.GetInstance().SetDilation(false, true, true);
                break;
            case SliderType.widthAndHeight:
                HallDilator.GetInstance().SetDilation(true, true, false);
                break;

        }
    }

    public void RoundValue()
    {
        if (percent > .9f)
        {
            knob.transform.position = end.transform.position;
        }
        if (percent < .1f)
        {
            knob.transform.position = start.transform.position;
        }
    }

    public void SetAtEnd()
    {
        if (!isActive)
        {
            knob.transform.position = end.transform.position;
        }
    }

    public void CheckReset()
    {
        if (!isActive)
        {
            ResetToStart();
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     // Debug.Log("HIT");
    //     if (other.CompareTag("Player"))
    //     {
    //         SetKnobTarget(other.transform);
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         ReleaseTarget();
    //     }
    // }

    // public void SetColliderInfo()
    // {
        // box.size = InfoManager.GetInstance().realWorld.hallDimensions;
        // float d  = InfoManager.GetInstance().realWorld.hallLength;
        // start.transform.localPosition = new Vector3(0, 0, -d);
        // end.transform.localPosition = new Vector3(0, 0, d);
    // }
}
