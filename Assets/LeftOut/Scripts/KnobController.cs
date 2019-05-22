using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class KnobController : MonoBehaviour
{
    // public float bounds = 20;

    void LateUpdate()
    {
        ConstrainMovement();
    }

    public Transform target;

    float lerpSpeed = 10;
    Vector3 targetPosition;

    void Update() {
        if (target != null) {
            targetPosition = target.transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime );
        }
    }

    void ConstrainMovement() {
        transform.localPosition = new Vector3(0, 0, Mathf.Clamp(transform.localPosition.z, start, end));
    }

    float start, end;

    public void SetBounds(float s, float e) {
        start = s;
        end = e;
    }
}
