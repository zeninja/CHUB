using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMod : MonoBehaviour {
    AudioMixer mixer;
    public enum ModType { ParamEQ, Chorus, Reverb, Distortion, }
    public ModType modType;
    public MetaSlider.StageInfo targetStage;
    public Extensions.Property range;

    [System.Serializable]
    public class StaticValue {
        public string name;
        public float value;
    }

    // [SerializeField]
    public List<StaticValue> staticValues = new List<StaticValue> ();

    void Start () {
        mixer = AudioManager.GetInstance ().mixer;
        
        GiantSlider.OnValueChanged += CheckValueChanged;
        GiantSlider.OnSliderStarted += StartSliderAudio;

        SetTargetValue(0); // init values
    }

    void CheckValueChanged () {
        if (targetStage.world == MetaSlider.GetInstance ().stageInfo.world &&
            targetStage.level == MetaSlider.GetInstance ().stageInfo.level) {
            SetTargetValue (MetaSlider.GetInstance ().currentSliderValue);
        }

    }

    void StartSliderAudio () {
        AudioManager.GetInstance ().PlayCurrentHall ();
        SetStaticValues ();

        // int hallIndex = MetaSlider.GetInstance().activeSliderIndex;
        // AudioManager.GetInstance().PlayStage(targetStage);
        // AudioManager.GetInstance().PlayHall(hallIndex);
    }

    void SetStaticValues () {
        foreach (StaticValue v in staticValues) {
            mixer.SetFloat (v.name, v.value);
        }
    }

    void SetTargetValue (float completion) {

        float currentValue = Extensions.mapRangeMinMax (0, 1, range.start, range.end, completion);

        switch (modType) {
            case ModType.Reverb:
            case ModType.ParamEQ:
            case ModType.Distortion:

                mixer.SetFloat (modType.ToString (), currentValue);

                break;

            case ModType.Chorus:
                // Special case for each of the 3 "Wet Mix" sliders
                for (int i = 1; i <= 3; i++) {
                    mixer.SetFloat ("WetMix" + i, currentValue);
                }

                break;
        }

        // Debug.Log ("Set " + modType.ToString () + " to " + currentValue);
    }

}