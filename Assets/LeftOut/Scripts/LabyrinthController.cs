using System.Collections;
using System.Collections.Generic;
using RaymarchingToolkit;
using UnityEngine;

public class LabyrinthController : MonoBehaviour
{

    public RaymarchObject voidBox;
    public RaymarchObject innrBox;
    public RaymarchObject marble;

    public enum DilationType { none, length, height }

    [System.Serializable]
    public class DilationInfo
    {
        public string name;
        public MetaSlider.StageInfo stageInfo;
        public DilationType dilationType;
        public AnimationCurve curve;
        public Extensions.Property range;
        public bool canBackslide;
    }

    [SerializeField]
    public DilationInfo[] dilationSettings;

    // DilationInfo currentInfo;

    void Start()
    {
        GiantSlider.OnValueChanged += ProcessDilation;
        // GiantSlider.OnBackslide += CheckBackslide;
        MetaSlider.OnActiveSliderChanged += SetCurrentSlider;

        foreach(DilationInfo d in dilationSettings) {
            if (d.stageInfo.world == 3 && d.stageInfo.level == 1) {
                d.range.start = InfoManager.GetInstance().wallHeight;
            }
        }

        startBox = voidBox.GetObjectInput("x").floatValue;
        innerSize = innrBox.GetObjectInput("x").floatValue;
    }

    public void ProcessDilation()
    {
        foreach (DilationInfo d in activeInfo)
        {
            DilationType dilationType = d.dilationType;
            float p = MetaSlider.GetInstance().GetCurrentSliderValue();

            switch (dilationType)
            {
                case DilationType.length:

                    bool levelIsEven = d.stageInfo.level % 2 == 0;
                    string dilationDir = levelIsEven ? "x" : "z";

                    float finalLength = GetCurvedValue(d, p);

                    voidBox.GetObjectInput(dilationDir).SetFloat(finalLength);
                    innrBox.GetObjectInput(dilationDir).SetFloat(finalLength - 0.52085f);

                    break;

                case DilationType.height:

                    float finalHeight = GetCurvedValue(d, p);
                    marble.GetObjectInput("y").SetFloat(finalHeight);
                    voidBox.GetObjectInput("y").SetFloat(finalHeight + 1);
                    innrBox.GetObjectInput("y").SetFloat(finalHeight);

                    break;

                case DilationType.none:

                    break;
            }
        }
    }

    // void CheckBackslide(float amt)
    // {
    //     if (currentInfo.canBackslide)
    //     {
    //         Debug.Log("BACKSLIDING");
    //         ProcessDilation();
    //     }
    // }

    public float GetCurvedValue(DilationInfo info, float t)
    {
        float diff = info.range.end - info.range.start;
        float final = info.range.start + info.curve.Evaluate(Mathf.Clamp01(t)) * diff;

        return final;
    }

    List<DilationInfo> activeInfo = new List<DilationInfo>();

    void SetCurrentSlider()
    {
        activeInfo.Clear();

        foreach (DilationInfo d in dilationSettings)
        {
            if (MetaSlider.GetInstance().StageInfoMatches(d.stageInfo))
            {
                activeInfo.Add(d);
                // Debug.Log("Added info " + d.stageInfo.world + "-" + d.stageInfo.level);
            }
        }

        // currentInfo = dilationSettings[MetaSlider.GetInstance().GetSliderIndex()];
    }

    float startBox = 1.18f;
    float startZ = 1.18f;

    float innerSize = 0.66665f;

    public void ResetBox()
    {
        voidBox.GetObjectInput("x").SetFloat(startBox);
        voidBox.GetObjectInput("z").SetFloat(startBox);

        innrBox.GetObjectInput("x").SetFloat(innerSize);
        innrBox.GetObjectInput("z").SetFloat(innerSize);
    }



    // GiantSlider prevSlider;

    // IEnumerator SmoothEnd(float p)
    // {
    //     while (p < 1)
    //     {
    //         p += Time.deltaTime;


    //         yield return new WaitForFixedUpdate();
    //     }

    // }
}