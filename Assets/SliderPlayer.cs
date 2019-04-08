using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPlayer : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed = 10f;

    Vector3 offset = new Vector3(300, 0, 0);

    void Update() {
        Vector3 targetPos = target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * lerpSpeed);
    }
}
