using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;


public class VoidLabyrinthController : MonoBehaviour
{
    public List<RaymarchObject> voidHalls = new List<RaymarchObject>();

    public RealLabyrinthController realLabyrinth;

    static int activeHallIndex;

    void Update()
    {
        UpdateHalls();
    }

    void UpdateHalls()
    {
        for (int i = 0; i < 4; i++)
        {
            voidHalls[i].GetObjectInput("x").SetFloat(realLabyrinth.info_RealWorld.hallWidth);
            voidHalls[i].GetObjectInput("y").SetFloat(realLabyrinth.info_RealWorld.hallHeight);
            voidHalls[i].GetObjectInput("z").SetFloat(realLabyrinth.info_RealWorld.hallLength);
        }
    }
}
