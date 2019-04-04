using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSlider : MonoBehaviour
{
    [Range(0, 1)]
    public float percent;
    Transform start, end, knob;
    public HallwayDilator dilator;

    public bool devMode = true;
    bool isActive;

    void Start() {
        // dilator = GetComponentInParent<HallwayDilator>();
        start = transform.Find("start");
        end   = transform.Find("end");
        knob  = transform.Find("knob");

        SetSliderInfo();
    }

    void LateUpdate() {
        if(!isActive && !devMode) { return; }
        SetPercent();
        SetDilator();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            isActive = true;
        }    
    }

    void SetSliderInfo() {
        // start.transform.position = new Vector3(0, 0,  HallwayInfo.hallLength);
        // end  .transform.position = new Vector3(0, 0, -HallwayInfo.hallLength);
        // knob.GetComponent<KnobController>().bounds = start.position.z;
    }

    void SetPercent() {
        percent = Extensions.mapRange(start.position.z, end.position.z, 0, 1, knob.position.z);
    }

    void SetDilator() {
        dilator.dilationPercent = percent;
    }
}
