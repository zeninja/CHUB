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

    public List<RaymarchObject> halls   = new List<RaymarchObject>();
    public List<RaymarchObject> corners = new List<RaymarchObject>();
    public List<GameObject> sliders = new List<GameObject>();

    public List<Vector3> orthographicPts = new List<Vector3>();
    public List<Vector3> cornerPts       = new List<Vector3>();

    void Awake()
    {
        Init();
    }

    void Init()
    {
        info_RealWorld.labyrinthWidth = info_RealWorld.hallLength + info_RealWorld.hallWidth;

        float x = info_RealWorld.labyrinthWidth;
        float z = info_RealWorld.labyrinthWidth;

        orthographicPts.Add(new Vector3(-x, 0, 0));
        orthographicPts.Add(new Vector3(0, 0, z));
        orthographicPts.Add(new Vector3(x, 0, 0));
        orthographicPts.Add(new Vector3(0, 0, -z));

        cornerPts.Add(new Vector3(-x, 0, z));
        cornerPts.Add(new Vector3(x, 0, z));
        cornerPts.Add(new Vector3(x, 0, -z));
        cornerPts.Add(new Vector3(-x, 0, -z));


        for(int i = 0; i < 4; i++) {
            sliderScripts[i] = sliders[i].GetComponent<GiantSlider>();
        }
    }

    void Update()
    {
        UpdatePoints();
        SetPosAndSize();

        if (dilateHalls)
        {
            hallDilator.UpdateHallDilation();
        }

        UpdateSliders();
    }

    void UpdatePoints()
    {

        float x = info_RealWorld.labyrinthWidth;
        float z = info_RealWorld.labyrinthWidth;

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
            halls[i].transform.localPosition   = orthographicPts[i];
            corners[i].transform.localPosition = cornerPts[i];
            sliders[i].transform.localPosition = orthographicPts[i];

            // w h l
            halls[i].GetObjectInput("x").SetFloat(info_RealWorld.hallWidth);
            halls[i].GetObjectInput("y").SetFloat(info_RealWorld.hallHeight);
            halls[i].GetObjectInput("z").SetFloat(info_RealWorld.hallLength);
            
            // w h w
            corners[i].GetObjectInput("radius").SetFloat(info_RealWorld.hallWidth);
 
        }

        maxHallLength = info_RealWorld.hallLength + info_RealWorld.hallWidth * 2;
    }

    void UpdateSliders() {
        
        for(int i = 0; i < hallDilationPct.Length; i++) {
            // float local  = hallDilationPct[i];
            // float slider = sliderScripts[i].percent;
            
            // if(slider != local) {
                hallDilationPct[i] = sliderScripts[i].percent;
            // }
        }
    }

    GiantSlider[] sliderScripts = new GiantSlider[4];

    [Range(0, 1)]
    public float[] hallDilationPct;
    public float   maxHallLength;

    public HallDilator hallDilator;
    public bool        dilateHalls;
}
