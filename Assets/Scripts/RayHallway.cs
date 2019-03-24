using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

[ExecuteInEditMode]
public class RayHallway : MonoBehaviour
{
    public float hallLength = 10;
    public float hallWidth = 5;            // keep private until i have a total labyrinth controller

    public float hallCutoutPct = .6f;

    public RaymarchObject containerBox;
    public RaymarchObject rawHallway;
    public RaymarchObject hallwayCutout;

    void Update() {
        SetHallCutoutSize();
        SetHallSize();
        SetContainerSize();
        SetContainerPosition();
    }

    void SetHallSize() {
        rawHallway.GetObjectInput("x").SetFloat(hallWidth);
        rawHallway.GetObjectInput("z").SetFloat(hallLength);
    }

    void SetHallCutoutSize() {
        hallwayCutout.GetObjectInput("x").SetFloat(hallWidth * hallCutoutPct);
        hallwayCutout.GetObjectInput("z").SetFloat(hallLength + 1);
    }

    void SetContainerSize() {
        // the container box is used for the Intersection calculation
        // a^2 + b^2 = c^2 
        // c = desierd hall length, a = box width/height
        // a = Sqrt(c^2/2)

        // float longerSider = Mathf.Max(hallLength, hallWidth);

        float containerSize = Mathf.Sqrt( (hallLength * hallLength) + (hallWidth * hallWidth));
        containerBox.GetObjectInput("x").SetFloat(containerSize);
        containerBox.GetObjectInput("z").SetFloat(containerSize);
    }

    void SetContainerPosition() {
        float x = rawHallway.transform.position.x + hallWidth;
        containerBox.transform.position = new Vector3(x, 0, 0);
    }
}
