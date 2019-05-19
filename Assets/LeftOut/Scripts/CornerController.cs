using System.Collections;
using System.Collections.Generic;
using RaymarchingToolkit;
using UnityEngine;

public class CornerController : MonoBehaviour
{

    RaymarchObject r;
    BoxCollider box;
    public GiantSlider slider;

    public enum CornerType { start, end };
    public CornerType cornerType;


    void Start()
    {
        r = GetComponent<RaymarchObject>();
        box = GetComponent<BoxCollider>();
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         if (cornerType == CornerType.start)
    //         {
    //             slider.HandleStartEntered();
    //             StartCoroutine(Flash(1));
    //         }

    //         if (cornerType == CornerType.end)
    //         {
    //             slider.HandleExitEntered();
    //             // StartCoroutine(Flash(1));
    //         }
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         if (cornerType == CornerType.start)
    //         {
    //             slider.HandleStartExited();

    //             // StartCoroutine(Flash(2));
    //         }
    //         if (cornerType == CornerType.end)
    //         {
    //             slider.HandleExitExited();

    //             // StartCoroutine(Flash(2));
    //         }
    //     }
    // }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cornerType == CornerType.start)
            {
                slider.StartCornerStay(other.transform);
            }

            if (cornerType == CornerType.end)
            {
                // Debug.Log("END CORNER STAY");
                slider.EndCornerStay();
            }
        }
    }

    // bool showSphere = false;

    // IEnumerator Flash(int times)
    // {
    //     if (GetComponentInParent<GiantSlider>().isActive)
    //     {
    //         for (int i = 0; i < times; i++)
    //         {
    //             showSphere = true;
    //             yield return new WaitForSeconds(.15f);
    //             showSphere = false;
    //         }
    //     }
    // }


    // void OnDrawGizmos()
    // {
    //     if (showSphere)
    //     {
    //         Gizmos.DrawWireSphere(transform.position, 1);
    //     }
    // }
}