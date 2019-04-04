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

    public RealWorldInfo info_RealWorld;

    public List<GameObject> halls   = new List<GameObject>();
    public List<GameObject> corners = new List<GameObject>();
    public List<GameObject> sliders = new List<GameObject>();

    public List<Vector3> orthographicPts = new List<Vector3>();
    public List<Vector3> cornerPts       = new List<Vector3>();

    void Start()
    {
        Init();
    }

    void Init()
    {
        info_RealWorld.labyrinthWidth = info_RealWorld.hallLength * 2;

        float x = info_RealWorld.labyrinthWidth / 2;
        float z = info_RealWorld.labyrinthWidth / 2;

        orthographicPts.Add(new Vector3(-x, 0, 0));
        orthographicPts.Add(new Vector3(0, 0, z));
        orthographicPts.Add(new Vector3(x, 0, 0));
        orthographicPts.Add(new Vector3(0, 0, -z));

        cornerPts.Add(new Vector3(-x, 0, z));
        cornerPts.Add(new Vector3(x, 0, z));
        cornerPts.Add(new Vector3(x, 0, -z));
        cornerPts.Add(new Vector3(-x, 0, -z));
    }

    void Update()
    {
        UpdatePoints();
        SetPosAndSize();

        if (dilateHalls)
        {
            hallDilator.UpdateHallDilation();
        }
    }

    void UpdatePoints()
    {
        info_RealWorld.labyrinthWidth = info_RealWorld.hallLength + info_RealWorld.hallWidth;

        float x = info_RealWorld.labyrinthWidth / 2;
        float z = info_RealWorld.labyrinthWidth / 2;

        orthographicPts[0] = new Vector3(-x, 0,  0);
        orthographicPts[1] = new Vector3( 0, 0,  z);
        orthographicPts[2] = new Vector3( x, 0,  0);
        orthographicPts[3] = new Vector3( 0, 0, -z);

        // -+ ++ +- --
        cornerPts[0] = new Vector3(-x, 0,  z);
        cornerPts[1] = new Vector3( x, 0,  z);
        cornerPts[2] = new Vector3( x, 0, -z);
        cornerPts[3] = new Vector3(-x, 0, -z);
    }

    void SetPosAndSize()
    {
        for (int i = 0; i < 4; i++)
        {
            // Set position
            halls[i].transform.localPosition = orthographicPts[i];
            corners[i].transform.localPosition = cornerPts[i];
            sliders[i].transform.localPosition = orthographicPts[i];
                    
            // Set Size
            halls[i].transform.localScale   = new Vector3(info_RealWorld.hallWidth, info_RealWorld.hallHeight, info_RealWorld.hallLength);
            corners[i].transform.localScale = new Vector3(info_RealWorld.hallWidth, info_RealWorld.hallHeight, info_RealWorld.hallWidth);
            sliders[i].transform.localScale = new Vector3(info_RealWorld.hallWidth, info_RealWorld.hallHeight, info_RealWorld.hallLength);
        }

        maxHallLength = info_RealWorld.hallLength + info_RealWorld.hallWidth * 2;

    }

    [Range(0, 1)]
    public float[] hallDilationPct;
    public float   maxHallLength;

    public HallDilator hallDilator;
    public bool        dilateHalls;
}
