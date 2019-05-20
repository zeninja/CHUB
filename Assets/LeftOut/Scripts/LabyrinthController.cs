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
        public MetaSlider.StageInfo stageInfo;
        public DilationType dilationType;
        public AnimationCurve curve;
        public Extensions.Property range;
        public bool canBackslide;
    }

    [SerializeField]
    public DilationInfo[] dilationSettings;

    DilationInfo currentInfo;

    void Start()
    {
        GiantSlider.OnValueChanged += ProcessDilation;
        GiantSlider.OnBackslide += CheckBackslide;
        MetaSlider.OnActiveSliderChanged += SetCurrentSlider;
    }

    public float smoothingSpeed = 10f;

    void ProcessDilation()
    {
        if (currentInfo.stageInfo.world == MetaSlider.GetInstance().stageInfo.world &&
            currentInfo.stageInfo.level == MetaSlider.GetInstance().stageInfo.level)
        {

            DilationType dilationType = currentInfo.dilationType;
            float p = MetaSlider.GetInstance().GetCurrentSliderValue();

            switch (dilationType)
            {

                case DilationType.length:

                    bool levelIsEven = currentInfo.stageInfo.level % 2 == 0;
                    string dilationDir = levelIsEven ? "x" : "z";

                    float finalLength = GetCurvedValue(currentInfo, p);

                    // float targetLength = GetCurvedValue(currentInfo, p);

                    // float outerBoxVal = voidBox.GetObjectInput(dilationDir).floatValue;
                    // float innrBoxVal = voidBox.GetObjectInput(dilationDir).floatValue;

                    voidBox.GetObjectInput(dilationDir).SetFloat(finalLength);
                    innrBox.GetObjectInput(dilationDir).SetFloat(finalLength - 1);

                    break;

                case DilationType.height:


                    // float targetHeight = GetCurvedValue(currentInfo, p);

                    // float marbleVal = marble.GetObjectInput("y").floatValue;
                    // float outerBox = marble.GetObjectInput("y").floatValue;
                    // float innerBox = marble.GetObjectInput("y").floatValue;

                    // marble .GetObjectInput("y").SetFloat(Mathf.Lerp(marbleVal, targetHeight, Time.deltaTime * smoothingSpeed));
                    // voidBox.GetObjectInput("y").SetFloat(Mathf.Lerp(outerBox, targetHeight + 1, Time.deltaTime * smoothingSpeed));
                    // innrBox.GetObjectInput("y").SetFloat(Mathf.Lerp(innerBox, targetHeight, Time.deltaTime * smoothingSpeed));


                    float finalHeight = GetCurvedValue(currentInfo, p);
                    marble.GetObjectInput("y").SetFloat(finalHeight);
                    voidBox.GetObjectInput("y").SetFloat(finalHeight + 1);
                    innrBox.GetObjectInput("y").SetFloat(finalHeight);

                    break;

                case DilationType.none:

                    break;
            }
        }
    }

    void CheckBackslide(float amt)
    {
        if (currentInfo.canBackslide)
        {
            ProcessDilation();
        }
    }

    public float GetCurvedValue(DilationInfo info, float t)
    {
        float diff = info.range.end - info.range.start;
        float final = info.range.start + info.curve.Evaluate(Mathf.Clamp01(t)) * diff;

        return final;
    }

    void SetCurrentSlider()
    {
        currentInfo = dilationSettings[MetaSlider.GetInstance().GetSliderIndex()];
    }
}