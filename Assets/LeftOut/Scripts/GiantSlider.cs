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
    }

    void LateUpdate()
    {
        if (!devMode && !isActive) { return; }

        GetPercentByKnob();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");
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
            RoundValue();
        }
    }

    public void ReleaseTarget()
    {
        knob.GetComponent<KnobController>().target = null;
        isActive = false;
    }

    void RoundValue()
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
        end  .transform.localPosition = new Vector3(0, 0,  d);

        // knob.GetComponent<KnobController>().bounds = d;
    }

    void Update() {
        // should be able to move this to start only eventually
        // TODO
        // REMOVE
        // DEBUG
        SetColliderInfo();
    }
}
