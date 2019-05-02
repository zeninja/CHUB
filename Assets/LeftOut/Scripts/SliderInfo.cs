using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderInfo : MonoBehaviour
{
    private static SliderInfo instance;
    public static SliderInfo GetInstance()
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



    public GiantSlider[] sliders = new GiantSlider[4];

    public float GetSlider(int index) {
        return sliders[index].percent;
    }
}
