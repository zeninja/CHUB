using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositioner : MonoBehaviour
{

    public Vector3 startPos;

    void Start()
    {
        ResetToBeginning(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            ResetToBeginning();
        }
    }

    public void ResetToBeginning() {
        transform.position = startPos;
    }
}
