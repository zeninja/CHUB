using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class KnobController : MonoBehaviour
{
    public float bounds = 20;

    void LateUpdate()
    {
        ConstrainMovement();
    }

    void ConstrainMovement() {
        transform.localPosition = new Vector3(0, 0, Mathf.Clamp(transform.localPosition.z, -bounds, bounds));
    }
}
