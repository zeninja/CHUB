using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MechanicManager : MonoBehaviour
{
    void OnEnable()
    {
        GiantSlider.OnValueChanged += OnValueChanged;
    }

    void OnDisable()
    {
        GiantSlider.OnValueChanged -= OnValueChanged;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnValueChanged()
    {
        
    }

    // [Range(0, 1f)]
    // public List<float> hallDilationPct = new List<float>(4);
    // public List<GiantSlider> sliders = new List<GiantSlider>(4);
    // float totalHallDilation;

    // void GetTotalHallDilation()
    // {
    //     for (int i = 0; i < hallDilationPct.Count; i++)
    //     {
    //         hallDilationPct[i] = sliders[i].percent;
    //     }

    //     totalHallDilation = hallDilationPct.Sum() / 4;
    // }
}
