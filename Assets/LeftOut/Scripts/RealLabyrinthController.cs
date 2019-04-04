using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;

public class RealLabyrinthController : MonoBehaviour
{

    [System.Serializable]
    public class RealWorldInfo
    {
        public float hallWidth = 5;      // x
        public float hallHeight = 5;     // y
        public float hallLength = 20;    // z
        public float labyrinthWidth;
    }

    public RealWorldInfo info_realWorld;
    GameObject realWorldContainer;
    List<RaymarchObject> hallSegments = new List<RaymarchObject>();

    void Start()
    {
        // SpawnRealWorld();
    }

    // void SpawnRealWorld()
    // {
    //     realWorldContainer = new GameObject("RealWorldContainer");
    //     realWorldContainer.transform.parent = transform;

    //     for (int i = 0; i < 4; i++)
    //     {
    //         RaymarchObject hallSegment = Instantiate(prefab_hallSegment);

    //         Quaternion rot = Quaternion.Euler(0, 90f * i, 0);
    //         Vector3 pos = hallSegment.transform.position + hallSegment.transform.right * -1 * info_realWorld.labyrinthWidth;

    //         hallSegment.GetComponent<HallSegment>().SetInfo(new Vector3(info_realWorld.hallWidth, info_realWorld.hallHeight, info_realWorld.hallLength));
    //         hallSegment.transform.position = pos;
    //         hallSegment.transform.rotation = rot;
    //         hallSegments.Add(hallSegment);

    //         hallSegment.enabled = true;
    //     }
    // }

    // void Update()
    // {
    //     CalculateLabyrinthWidth();
    //     // UpdateHallInfo();
    // }

    // void CalculateLabyrinthWidth()
    // {
    //     info_realWorld.labyrinthWidth = (info_realWorld.hallLength * 2) - (info_realWorld.hallWidth / 2);
    // }

    void UpdateHallInfo()
    {
        for (int i = 0; i < hallSegments.Count; i++)
        {
            RaymarchObject hallSegment = hallSegments[i];

            Quaternion rot = Quaternion.Euler(0, 90 * i, 0);
            Vector3 pos = hallSegment.transform.position + hallSegment.transform.right * -1 * info_realWorld.labyrinthWidth;

            hallSegment.GetObjectInput("x").SetFloat(info_realWorld.hallWidth);
            hallSegment.GetObjectInput("y").SetFloat(info_realWorld.hallHeight);
            hallSegment.GetObjectInput("z").SetFloat(info_realWorld.hallLength);

            hallSegments.Add(hallSegment);
        }
    }
}
