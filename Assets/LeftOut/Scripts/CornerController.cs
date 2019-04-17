using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class CornerController : MonoBehaviour
{
    RaymarchObject r;
    BoxCollider box;
    public GiantSlider slider;

    public enum CornerType { start, end };
    public CornerType cornerType;


    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<RaymarchObject>();
        box = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // CheckCornerSize();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cornerType == CornerType.start)
            {
                slider.CheckReset();
            }

            if (cornerType == CornerType.end)
            {
                // slider.CheckReset();
                // slider.SetAtEnd();
                // slider.RoundValue();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // if (cornerType == CornerType.start)
            // {
            //     slider.CheckReset();
            // }
            if (cornerType == CornerType.end)
            {
                // slider.RoundValue();
                // slider.TryReleaseTarget();
                slider.CheckReset();
            }
        }
    }

    // void CheckCornerSize()
    // {
    //     Debug.Log("Corner scale not changing");
    //     // box.size = RealLabyrinthController.GetInstance().info_RealWorld.cornerDimensions;
    // }
}
