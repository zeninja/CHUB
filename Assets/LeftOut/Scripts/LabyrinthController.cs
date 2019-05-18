using System.Collections;
using System.Collections.Generic;
using RaymarchingToolkit;
using UnityEngine;

public class LabyrinthController : MonoBehaviour {

    public RaymarchObject voidBox;
    public RaymarchObject innrBox;

    public enum DilationType { width, height, none }

    [System.Serializable]
    public class DilationInfo {
        public MetaSlider.StageInfo stageInfo;
        public DilationType dilationType;
        public AnimationCurve curve;
        public float dilationAmt;

        public bool canBackslide;
    }

    [SerializeField]
    public DilationInfo[] dilationSettings;

    DilationInfo currentDilationInfo;


    public float baseScale = 1.5f;


    void ProcessDilation () {
        DilationInfo info         = dilationSettings[MetaSlider.GetInstance().GetSliderIndex()];
        DilationType dilationType = info.dilationType;
        float amt                 = info.dilationAmt;
        AnimationCurve curve      = info.curve;
        float p                   = MetaSlider.GetInstance().GetCurrentSliderValue();

        switch (dilationType) {
            case DilationType.width:

                bool levelIsEven   = info.stageInfo.level % 2 == 0;
                string dilationDir = levelIsEven ? "x" : "z"; 
                float finalWidth   = baseScale + amt * curve.Evaluate(p);

                voidBox.GetObjectInput(dilationDir).SetFloat(finalWidth);
                break;
            case DilationType.height:

                float finalHeight  = amt * curve.Evaluate(p);
                voidBox.GetObjectInput("y").SetFloat(finalHeight);

                break;
            case DilationType.none:

                break;
        }

    }

    void SetWallHeight () {
        float wallHeight = 0;
        voidBox.GetObjectInput ("y").SetFloat (wallHeight);
        innrBox.GetObjectInput ("y").SetFloat (wallHeight);
    }
}