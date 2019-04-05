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


    void Start() {
        start = transform.Find("start");
        end   = transform.Find("end");
        knob  = transform.Find("knob");

        box   = GetComponent<BoxCollider>();
    }

    void LateUpdate() {
        if(!isActive && !devMode) { return; }

        SetPercent();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            SetKnobTarget(other.transform);
        }    
    }

    public void ReleaseTarget() {
        target = null;
        isActive = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            ReleaseTarget();
        }    
    }

    void SetKnobTarget(Transform target) {
        isActive = true;
        knob.GetComponent<KnobController>().target = target;
    }

    void HandlePlayerExit() {

    }

    void SetPercent() {
        percent = Extensions.mapRange(start.position.z, end.position.z, 0, 1, knob.position.z);
    }

    public void SetColliderInfo(Vector3 info) {
        box.size = info * 2;
    }
}
