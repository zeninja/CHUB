using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class CornerController : MonoBehaviour
{
    RaymarchObject r;
    BoxCollider box;

    float cornerScale;

    // Start is called before the first frame update
    void Start()
    {
        r   = GetComponent<RaymarchObject>();
        box = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        CheckCornerSize();
    }

    void CheckCornerSize() {
        float cornerScale = r.GetObjectInput("radius").floatValue;
        
        if (box.size.x != cornerScale) {
            box.size = Vector3.one * cornerScale * 2;  
        }
    }

    // GameObject nextHallKnob;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("Corner hit, but no code here");
            // RealLabyrinthController.HandleCornerHit(nextHallKnob);
        }
    }
}
