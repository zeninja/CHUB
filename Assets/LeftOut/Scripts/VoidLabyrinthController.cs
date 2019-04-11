using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;


public class VoidLabyrinthController : MonoBehaviour
{
    public List<RaymarchObject> voidHalls = new List<RaymarchObject>();
    public RealLabyrinthController realLabyrinth;
    public RaymarchObject ground;


    void Update() {
        SetHallPositions();
        SetHallDimenions();
        SetGroundInfo();
    }

    void SetHallPositions() {

        Vector3 heightCompensator = new Vector3(0, realLabyrinth.info_RealWorld.hallHeight / 2, 0);

        for (int i = 0; i < 4; i++)
        {
            voidHalls[i].transform.position = realLabyrinth.orthographicPts[i] + heightCompensator;
        }
    }

    void SetHallDimenions()
    {
        for (int i = 0; i < 4; i++)
        {
            voidHalls[i].GetObjectInput("x").SetFloat(realLabyrinth.info_RealWorld.hallWidth);
            voidHalls[i].GetObjectInput("y").SetFloat(realLabyrinth.info_RealWorld.hallHeight + 1);
            voidHalls[i].GetObjectInput("z").SetFloat(realLabyrinth.info_RealWorld.hallLength + realLabyrinth.info_RealWorld.hallWidth * 2);
        }
    }

    void SetGroundInfo() {
        ground.GetObjectInput("y").SetFloat(realLabyrinth.info_RealWorld.hallHeight);
    }

}
