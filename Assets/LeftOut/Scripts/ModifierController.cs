using System.Collections;
using System.Collections.Generic;
using RaymarchingToolkit;
using UnityEngine;

public class ModifierController : MonoBehaviour {

    public int targetWorld = 0;

    public enum SnippetType { V1, V2, V3 }
    public SnippetType snippetType = SnippetType.V3;

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

    RaymarchModifier obj;

    void Start () {
        obj = GetComponent<RaymarchModifier> ();

        randomStart = Random.Range (0, 1);
    }

    float randomStart;

    void Update () {

        if (MetaSlider.GetInstance ().stageInfo.world == targetWorld) {
            percent = MetaSlider.GetInstance ().worldLevelCompletionPct;
        }

        if (autoOscillate) {
            // percent = (Mathf.Sin (autoAmplitude * Time.time) + 1) / 2;
            percent += Mathf.PerlinNoise (Time.time, randomStart);

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
        switch (snippetType) {
            case SnippetType.V1:
                obj.GetInput (target).SetFloat (noise.x);
                break;
            case SnippetType.V2:
                obj.GetInput (target).SetVector3 ((Vector2) noise);
                break;
            case SnippetType.V3:
                obj.GetInput (target).SetVector3 (noise);
                break;
        }
    }
}