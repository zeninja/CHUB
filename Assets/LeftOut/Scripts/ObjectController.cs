using System.Collections;
using System.Collections.Generic;
using RaymarchingToolkit;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public enum SnippetType { V1, V2, V3 };
    public SnippetType snippetType = SnippetType.V3;

    public enum SliderIndex { zero, one, two, three, none };
    public SliderIndex sliderIndex = SliderIndex.none;

    public string target;

    public bool autoOscillate;
    public float autoAmplitude = 3;

    [Range (0, 1)]
    public float percent;

    public AnimationCurve xCurve;
    public AnimationCurve yCurve;
    public AnimationCurve zCurve;

    public Extensions.Property xRange;
    public Extensions.Property yRange;
    public Extensions.Property zRange;

    RaymarchObject obj;

    void Start () {
        obj = GetComponent<RaymarchObject> ();
    }

    void Update () {

        if (autoOscillate) {
            percent = (Mathf.Sin (autoAmplitude * Time.time) + 1) / 2;
        }

        if(sliderIndex != SliderIndex.none) {
            percent = SliderInfo.GetInstance().GetSlider((int)sliderIndex);
        }

        float x = GetFloatValue (xCurve, xRange);
        float y = GetFloatValue (yCurve, yRange);
        float z = GetFloatValue (zCurve, zRange);

        Vector3 noise = new Vector3 (x, y, z);

        SetObjectInput (noise);
    }

    float GetX () {
        return xRange.start + xCurve.Evaluate (percent) * (xRange.end - xRange.start);
    }

    float GetFloatValue (AnimationCurve curve, Extensions.Property p) {
        return p.start + curve.Evaluate (percent) * (p.end - p.start);
    }

    void SetObjectInput (Vector3 noise) {
        // obj.GetObjectInput (target).SetVector3 ((Vector2) noise);

        switch (snippetType) {
            case SnippetType.V1:
                obj.GetObjectInput (target).SetFloat (noise.x);
                break;
            case SnippetType.V2:
                obj.GetObjectInput (target).SetVector3 ((Vector2) noise);
                break;
            case SnippetType.V3:
                obj.GetObjectInput (target).SetVector3 (noise);
                break;
        }
    }
}