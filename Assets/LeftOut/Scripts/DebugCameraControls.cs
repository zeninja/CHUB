using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraControls : MonoBehaviour
{


    public float moveSpeed = 10;

    void Update()
    {

        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.S)) {
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }

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
