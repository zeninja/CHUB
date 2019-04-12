using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;


public class VoidLabyrinthController : MonoBehaviour
{
    public List<RaymarchObject> voidHalls = new List<RaymarchObject>();
    public RealLabyrinthController realLabyrinth;
    public RaymarchObject ground;


    void Start()
    {
        StartHallDimensions();
        // SetHallPositions();
    }

    void Update()
    {
    //     // SetHallDimenions();
        SetHallPositions();
    //     // SetGroundInfo();
    }

    void SetHallPositions()
    {

        Vector3 heightCompensator = new Vector3(0, realLabyrinth.info_RealWorld.hallHeight / 2, 0);
        Vector3 widthCompensator  = new Vector3(HallDilator.GetDilatedWidth() / 2f, 0, 0);

        Debug.Log(widthCompensator);

        for (int i = 0; i < 4; i++)
        {
            Vector3 orthoPt = realLabyrinth.orthographicPts[i] * 2;
            voidHalls[i].transform.localPosition = orthoPt + widthCompensator;
            Debug.Log("SETTING HALL POSITIONS");
            // + heightCompensator + widthCompensator; //+ realLabyrinth.orthographicPts[i].normalized * voidHalls[i].GetObjectInput("x").floatValue + heightCompensator;
        }
    }

    void StartHallDimensions()
    {
        for (int i = 0; i < 4; i++)
        {
            voidHalls[i].GetObjectInput("x").SetFloat(realLabyrinth.info_RealWorld.hallWidth);
            voidHalls[i].GetObjectInput("y").SetFloat(realLabyrinth.info_RealWorld.hallHeight + 1);
            voidHalls[i].GetObjectInput("z").SetFloat(realLabyrinth.info_RealWorld.totalHallLength);
        }
    }

    // void SetGroundInfo()
    // {
    //     ground.GetObjectInput("y").SetFloat(realLabyrinth.info_RealWorld.hallHeight);
    // }

}
