using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{


    public class AudioAdjustment {
        public string target;
        public Extensions.Property range;
        public AnimationCurve curve;

        public enum AdjustmentType { triggered, slider };
        public AdjustmentType adjustmentType;

        
        public int sliderIndex;

    }

}