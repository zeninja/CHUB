using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;


public class VoidLabyrinthController : MonoBehaviour
{
    public List<RaymarchObject> voidHalls = new List<RaymarchObject>();
    public RealLabyrinthController realLabyrinth;

    // void Update()
    // {
    //     // UpdateHalls();
    // }


    void SetHallDimenions()
    {
        for (int i = 0; i < 4; i++)
        {
            voidHalls[i].GetObjectInput("x").SetFloat(realLabyrinth.info_RealWorld.hallWidth);
            voidHalls[i].GetObjectInput("y").SetFloat(realLabyrinth.info_RealWorld.hallHeight);
            voidHalls[i].GetObjectInput("z").SetFloat(realLabyrinth.info_RealWorld.hallLength + realLabyrinth.info_RealWorld.hallWidth * 2);
        }
    }

    void Start() {
        SetHallPositions();
        SetHallDimenions();
    }

    void SetHallPositions() {
        for (int i = 0; i < 4; i++)
        {
            voidHalls[i].transform.position = realLabyrinth.orthographicPts[i];
        }
    }
}
