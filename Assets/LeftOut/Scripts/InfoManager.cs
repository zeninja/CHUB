using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{

    private static InfoManager instance;
    public static InfoManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        Init();
    }


    [System.Serializable]
    public class RealWorldInfo
    {
        public float hallWidth = 5;      // x
        public float hallHeight = 5;     // y
        public float hallLength = 20;    // z
        public float distanceToInnerWall;
        public Vector3 hallDimensions;
        public Vector3 cornerDimensions;
    }

    public RealWorldInfo realWorld;
    public VoidInfo voidWorld;
    [System.Serializable]
    public class VoidInfo
    {
        public float hallLength;
        public float hallWidth;
        public float hallHeight;
        public Vector3 baseHallDimensions;   // This is the dimensions of the hallway (a complete hallway = hall + 2 corners)
                                             // It gets subtracted from the "marble" of the labyrinth to make the halls

                                             
        // public float distToHall_frwd;
        // public float distToHall_hztl;


    }

    public float wallHeight;

    public List<Vector3> orthographicPts = new List<Vector3>();
    public List<Vector3> cornerPts = new List<Vector3>();

    void Init()
    {
        UpdatePoints();
        SetHallDimensions();
        SetVoidHallDimensions();
    }

    void Update()
    {
        UpdatePoints();
        // SetHallDimensions();
        // SetVoidHallDimensions();
    }

    bool firstTime = true;

    void UpdatePoints()
    {
        realWorld.distanceToInnerWall = realWorld.hallLength / 2;

        float x = realWorld.distanceToInnerWall + realWorld.hallWidth / 2;
        float z = realWorld.distanceToInnerWall + realWorld.hallWidth / 2;

        if (firstTime)
        {
            orthographicPts.Add(new Vector3(-x, 0, 0));
            orthographicPts.Add(new Vector3(0, 0, z));
            orthographicPts.Add(new Vector3(x, 0, 0));
            orthographicPts.Add(new Vector3(0, 0, -z));

            // -+ ++ +- --
            cornerPts.Add(new Vector3(-x, 0, z));
            cornerPts.Add(new Vector3(x, 0, z));
            cornerPts.Add(new Vector3(x, 0, -z));
            cornerPts.Add(new Vector3(-x, 0, -z));

            firstTime = false;
        }
        else
        {
            orthographicPts[0] = new Vector3(-x, 0, 0);
            orthographicPts[1] = new Vector3(0, 0, z);
            orthographicPts[2] = new Vector3(x, 0, 0);
            orthographicPts[3] = new Vector3(0, 0, -z);

            // -+ ++ +- --
            cornerPts[0] = new Vector3(-x, 0, z);
            cornerPts[1] = new Vector3(x, 0, z);
            cornerPts[2] = new Vector3(x, 0, -z);
            cornerPts[3] = new Vector3(-x, 0, -z);
        }
    }

    void SetVoidHallDimensions()
    {
        float x = voidWorld.hallWidth  = (realWorld.hallWidth) / 2;
        float y = voidWorld.hallHeight = (realWorld.hallHeight / 2);
        float z = voidWorld.hallLength = (realWorld.hallLength + realWorld.hallWidth * 2) / 2;

        voidWorld.baseHallDimensions = new Vector3(x, y, z);

        // voidWorld.distToHall_frwd = realWorld.hallLength + realWorld.hallWidth / 2;
        // voidWorld.distToHall_hztl = realWorld.hallLength + realWorld.hallWidth / 2;
    }

    void SetHallDimensions()
    {
        realWorld.hallDimensions = new Vector3(realWorld.hallWidth, realWorld.hallHeight, realWorld.hallLength);
    }

    void SetCornerDimensions()
    {
        realWorld.cornerDimensions = new Vector3(realWorld.hallWidth, realWorld.hallHeight, realWorld.hallWidth);
    }

    [System.Serializable]
    public class GizmoInfo
    {
        public float sphereRadius = .15f;
    }

    public GizmoInfo gizmoInfo;

    void OnDrawGizmos()
    {
        for (int i = 0; i < 4; i++)
        {
            if (orthographicPts.Count != 4) { return; }


            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(orthographicPts[i], gizmoInfo.sphereRadius);
            // Gizmos.color = Color.yellow;
            // Gizmos.DrawWireSphere(realLabyrinth.orthographicPts[i], .25f);
        }
    }
}
