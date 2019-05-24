using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class WallHeightManager : MonoBehaviour
{
    void Start()
    {
        GetComponent<RaymarchObject>().GetObjectInput("y").SetFloat(InfoManager.GetInstance().wallHeight);        
    }
}
