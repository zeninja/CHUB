using System.Collections;
using System.Collections.Generic;
using RaymarchingToolkit;
using UnityEngine;

public class CornerController : MonoBehaviour {
    
    RaymarchObject r;
    BoxCollider box;
    public GiantSlider slider;

    public enum CornerType { start, end };
    public CornerType cornerType;

    void Start () {
        r = GetComponent<RaymarchObject> ();
        box = GetComponent<BoxCollider> ();
    }

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player")) {
            if (cornerType == CornerType.start) {
                slider.HandleStartEntered ();
            }

            if (cornerType == CornerType.end) {
                slider.HandleExitEntered ();
            }
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            if (cornerType == CornerType.start) {
                slider.HandleStartExited ();
            }
            if (cornerType == CornerType.end) {
                slider.HandleExitExited ();
            }
        }
    }
}