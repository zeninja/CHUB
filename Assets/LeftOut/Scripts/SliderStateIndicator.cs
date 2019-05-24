using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderStateIndicator : MonoBehaviour {

    public bool showState = false;

    MeshRenderer m;

    public Color prep, active, ending, completed;

    // Start is called before the first frame update
    void Start () {
        m = GetComponent<MeshRenderer> ();
    }

    // Update is called once per frame
    void Update () {
        m.enabled = showState;

        Color currentColor = new Color ();

        GiantSlider.SliderState state = GetComponent<GiantSlider> ().state;

        switch (state) {
            case GiantSlider.SliderState.active:
                currentColor = active;
                break;
            case GiantSlider.SliderState.prep:
                currentColor = prep;
                break;
            case GiantSlider.SliderState.completed:
                currentColor = completed;
                break;
            case GiantSlider.SliderState.ending:
                currentColor = ending;
                break;
        }

        m.material.color = currentColor;
    }
}