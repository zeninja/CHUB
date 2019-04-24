using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraControls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -90f, 0));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(new Vector3(0,  90f, 0));
        }
    }
}
