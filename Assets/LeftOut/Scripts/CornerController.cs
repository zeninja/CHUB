using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class CornerController : MonoBehaviour
{
    RaymarchObject r;
    BoxCollider box;
    public GiantSlider sliderToReset;
    public GiantSlider sliderToPrep;


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

    void CheckCornerSize()
    {
        box.size = RealLabyrinthController.GetInstance().info_RealWorld.cornerDimensions;

        // float cornerScale = r.GetObjectInput("radius").floatValue;
        // if (box.size.x != cornerScale)
        // {
        //     box.size = Vector3.one * cornerScale * 2;
        // }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // sliderToReset.SetAtEnd();
            sliderToPrep.PrepSlider();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sliderToReset.CheckReset();
        }
    }

}
