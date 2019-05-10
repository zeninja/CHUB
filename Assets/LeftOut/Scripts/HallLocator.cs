using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class HallLocator : MonoBehaviour
{

    private static HallLocator instance;
    public static HallLocator GetInstance() { 
        return instance;
    }

    void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            if (this != instance) {
                Destroy(this.gameObject);
            }
        }
    }

    public RaymarchObject[] halls;
}
