using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallController : MonoBehaviour
{
    public enum DilationType { none, length, width, height, lengthAndWidth, lengthAndHeight, widthAndHeight, all };
    public DilationType dilationType = DilationType.length;
    public AnimationCurve curve;

    // void OnEnable()
    // {
    //     GiantSlider.OnValueChanged += OnValueChanged;
    // }

    // void OnDisable()
    // {
    //     GiantSlider.OnValueChanged -= OnValueChanged;
    // }

    public void SetDilationType()
    {
        // if(isActive) { return; }    // only change type if not currently being used

        switch (dilationType)
        {
            // width, height, length
            case DilationType.length:
                HallDilator.GetInstance().SetDilation(false, false, true);
                break;
            case DilationType.width:
                HallDilator.GetInstance().SetDilation(true, false, false);
                break;
            case DilationType.height:
                HallDilator.GetInstance().SetDilation(false, true, false);
                break;
            case DilationType.lengthAndWidth:
                HallDilator.GetInstance().SetDilation(true, false, true);
                break;
            case DilationType.lengthAndHeight:
                HallDilator.GetInstance().SetDilation(false, true, true);
                break;
            case DilationType.widthAndHeight:
                HallDilator.GetInstance().SetDilation(true, true, false);
                break;
            case DilationType.all:
                HallDilator.GetInstance().SetDilation(true, true, true);
                break;
            case DilationType.none:
                HallDilator.GetInstance().SetDilation(false, false, false);
                break;
        }
    }

    // void OnValueChanged() {
        
    //     Debug.Log("Slider value changed");
    // }
}
