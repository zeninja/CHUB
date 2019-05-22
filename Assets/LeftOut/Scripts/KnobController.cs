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

    void Update() {
        if (target != null) {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, lerpSpeed * Time.deltaTime );
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
