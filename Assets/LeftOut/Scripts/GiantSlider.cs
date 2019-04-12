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

    // public enum AlignmentAxis { x, z };
    // public AlignmentAxis alignmentAxis = AlignmentAxis.z;

    RealLabyrinthController lab;

    void Start()
    {
        start = transform.Find("start");
        end = transform.Find("end");
        knob = transform.Find("knob");
        box = GetComponent<BoxCollider>();

        // Don't love this but here we are
        lab = GetComponentInParent<RealLabyrinthController>();
        SetColliderInfo();

        SetSize();
        ResetValue();

    }

    void SetSize()
    {
        start.transform.localPosition = new Vector3(0, 0, -lab.info_RealWorld.hallLength / 2);
        end.transform.localPosition   = new Vector3(0, 0,  lab.info_RealWorld.hallLength / 2);
        box.size = lab.info_RealWorld.hallDimensions;
        knob.GetComponent<KnobController>().bounds = lab.info_RealWorld.hallLength / 2 ;
    }

    void LateUpdate()
    {
        if (!devMode && !isActive) { return; }

        GetPercentByKnob();
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("HIT");
        if (other.CompareTag("Player"))
        {
            SetKnobTarget(other.transform);
        }
    }

    void SetKnobTarget(Transform target)
    {
        isActive = true;
        knob.GetComponent<KnobController>().target = target;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReleaseTarget();
            // RoundValue();
            // ResetValue();
        }
    }

    public void ReleaseTarget()
    {
        knob.GetComponent<KnobController>().target = null;
        isActive = false;
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
            ResetValue();
        }
    }

    public void ResetValue()
    {
        Debug.Log("Value reset");
        knob.transform.position = start.transform.position;
    }

    // void RoundValue()
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

    void GetPercentByKnob()
    {
        percent = Extensions.mapRange(start.localPosition.z, end.localPosition.z, 0, 1, knob.localPosition.z);
    }

    public void SetColliderInfo()
    {
        box.size = lab.info_RealWorld.hallDimensions * 2;

        // float d = alignmentAxis == AlignmentAxis.x ? lab.info_RealWorld.hallDimensions.x : lab.info_RealWorld.hallDimensions.z;


        float d = lab.info_RealWorld.hallLength;
        start.transform.localPosition = new Vector3(0, 0, -d);
        end.transform.localPosition = new Vector3(0, 0, d);

        // knob.GetComponent<KnobController>().bounds = d;
    }

    public void PrepSlider()
    {
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

    // void Update()
    // {
    //     // should be able to move this to start only eventually
    //     // TODO
    //     // REMOVE
    //     // DEBUG
    //     // SetColliderInfo();
    // }
}
