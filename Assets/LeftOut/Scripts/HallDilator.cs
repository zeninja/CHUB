using System.Collections;
using System.Collections.Generic;
using RaymarchingToolkit;
using UnityEngine;

public class HallDilator : MonoBehaviour
{

    private static HallDilator instance;
    public static HallDilator GetInstance()
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
    }
    public RaymarchObject labyrinthMarble;
    public float dilationAmount = 10;

    void Start()
    {
        FindLabyrinthMarbleAndHalls();
        // init values by "dilating halls"
        // they dont actually dilate bc the values are 0
        DilateHalls();
    }

    void FindLabyrinthMarbleAndHalls() {
        labyrinthMarble = GameObject.FindGameObjectWithTag("Marble").GetComponent<RaymarchObject>();
        dilatedHalls    = HallLocator.GetInstance().halls;
        sliders         = MetaSlider.GetInstance().sliders;
        hallControllers = MetaSlider.GetInstance().GetComponentsInChildren<HallController>();
    }

    public bool dilateLength;
    public bool dilateHeight;
    public bool dilateWidth;

    [SerializeField]
    public Vector3[] dilatedDimensions = new Vector3[4];

    // Vector3 AdjustHeightPos(int i)
    // {
    //     float dilatehdHeight = dilatedDimensions[i].y;

    //     Vector3 adjustedPos = new Vector3(0, dilatehdHeight / 2, 0);
    //     return adjustedPos;
    // }

    public void SetDilation(bool w, bool h, bool l)
    {
        dilateWidth = w;
        dilateHeight = h;
        dilateLength = l;
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < 4; i++)
        {
            if (InfoManager.GetInstance().orthographicPts.Count == 0) { return; }
            Gizmos.color = Color.yellow;
            // Gizmos.DrawWireSphere(centers[i].transform.position, .125f);
            // Gizmos.DrawWireSphere(InfoManager.GetInstance().orthographicPts[i] + InfoManager.GetInstance().orthographicPts[i].normalized * dilatedDimensions[i].x, .125f);
        }
    }

    void OnEnable()
    {
        GiantSlider.OnValueChanged += GetSliderValues;
        GiantSlider.OnValueChanged += DilateHalls;
    }

    void OnDisable()
    {
        GiantSlider.OnValueChanged -= GetSliderValues;
        GiantSlider.OnValueChanged -= DilateHalls;
    }

    // ----------------------------------------------------------------------------------

    public HallController[] hallControllers;

    public void DilateHalls()
    {
        List<float> dilatedHeights = new List<float>();

        float startHallWidth  = InfoManager.GetInstance().voidWorld.hallWidth;
        float startHallLength = InfoManager.GetInstance().voidWorld.hallLength;
        float startHallHeight = InfoManager.GetInstance().voidWorld.hallHeight;

        Debug.Log("Starthall height:" + startHallHeight);

        float dilatedWidth  = startHallWidth;
        float dilatedLength = startHallLength;
        float dilatedHeight = startHallHeight;

        for (int i = 0; i < 4; i++)
        {
            HallController h = hallControllers[i];
            RaymarchObject d = dilatedHalls[i];
            AnimationCurve c = h.curve;

            dilatedDimensions[i] = new Vector3(startHallWidth, startHallHeight, startHallLength);

            float dilationPct = GetDilationPct(i);
            float easedSlider = c.Evaluate(dilationPct);

            if (dilateWidth)
            {
                dilatedWidth = startHallWidth + dilationAmount * easedSlider;
            }

            if (dilateHeight)
            {
                dilatedHeight = startHallHeight + dilationAmount * easedSlider;
                dilatedHeights.Add(dilatedHeight);
            }

            if (dilateLength)
            {
                dilatedLength = startHallLength + dilationAmount * easedSlider;
            }

            // if (dilateWidth && dilateLength)
            // {
            //     // dilatedDimensions[i].z = 10;
            //     // float w = (dilatedLength + dilatedWidth * 2) / 2;
            //     // dilatedDimensions[i].z = w;
            //     // Debug.Log("Dilating oth");
            // }

            dilatedDimensions[i] = new Vector3(dilatedWidth, dilatedHeight, dilatedLength);

            // alwaysSyncHeight
            {
                float highestY = Mathf.Max(dilatedHeights.ToArray());
                dilatedDimensions[i].y = highestY;

                // Also dilate the labyrinth marble so that it actually LOOKS taller
                labyrinthMarble.GetObjectInput("y").SetFloat(highestY);
            }

            d.GetObjectInput("x").SetFloat(dilatedDimensions[i].x);
            d.GetObjectInput("y").SetFloat(dilatedDimensions[i].y);
            d.GetObjectInput("z").SetFloat(dilatedDimensions[i].z);

            // Adjust the positions to compensate for the width offset
            d.transform.localPosition = GetAdjustedPosition(i);

            FindHallCenters();
        }
    }

    Vector3 GetAdjustedPosition(int i)
    {
        Vector3 orthDir     = InfoManager.GetInstance().orthographicPts[i].normalized; // x, z only
        float distTowall    = InfoManager.GetInstance().realWorld.distanceToInnerWall;
        float dilatedWidth  = dilatedDimensions[i].x;
        float halvedWidth   = dilatedWidth / 2;
        float dilatedHeight = dilatedDimensions[i].y;
        float dilatedLength = dilatedDimensions[i].z;

        Vector3 adjustedPos = orthDir * (distTowall + dilatedWidth) + Vector3.up * (dilatedHeight / 2f);
        return adjustedPos;
    }

    public RaymarchObject[] dilatedHalls = new RaymarchObject[4];

    [Range(0, 1)]
    public float[] hallDilationPct = new float[4];
    public bool syncHalls;

    float GetDilationPct(int i)
    {
        if (syncHalls)
        {
            return Mathf.Max(hallDilationPct);
        }
        else
        {
            return hallDilationPct[i];
        }
    }

    public GiantSlider[] sliders;

    void GetSliderValues()
    {
        for (int i = 0; i < hallDilationPct.Length; i++)
        {
            hallDilationPct[i] = sliders[i].percent;
        }
    }

    public List<GameObject> centers;
    void FindHallCenters()
    {
        for (int i = 0; i < 4; i++)
        {
            if (InfoManager.GetInstance().orthographicPts.Count == 0) { return; }

            if (centers[i] == null) {
                centers[i] = new GameObject();
            } else {
                Vector3 dir  = InfoManager.GetInstance().orthographicPts[i];
                Vector3 norm = InfoManager.GetInstance().orthographicPts[i].normalized;
                float innerWall = InfoManager.GetInstance().realWorld.distanceToInnerWall;
                float dilation = dilatedDimensions[i].x;

                centers[i].transform.position = norm * (innerWall + dilation);

                if(i == 0) {
                    // Debug.Log(innerWall + "\n" + norm + "\n" + dilation);
                }
            }

        }
    }
}