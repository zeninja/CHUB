using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXController : MonoBehaviour
{
    public AudioMixer mixer;

    public enum ModType { ParamEQ, Chorus, Reverb, Distortion, }

    [System.Serializable]
    public class AudioMod
    {
        // public string name;
        public ModType modType;
        public MetaSlider.StageInfo targetStage;
        public Extensions.Property range;
    }

    [SerializeField]
    public List<AudioMod> audioMods;

    void Start()
    {
        GiantSlider.OnValueChanged  += CheckValueChanged;
        // GiantSlider.OnSliderStarted += StartSliderAudio;
    }

    void CheckValueChanged()
    {
        for (int i = 0; i < audioMods.Count; i++)
        {
            AudioMod mod = audioMods[i];

            if (mod.targetStage.world == MetaSlider.GetInstance().stageInfo.world && mod.targetStage.level == MetaSlider.GetInstance().stageInfo.level)
            {
                SetTargetValue(mod, MetaSlider.GetInstance().currentSliderValue);
            }

        }
    }

    // void StartSliderAudio()
    // {

    //     int hallIndex = MetaSlider.GetInstance().activeSliderIndex;
    //     // AudioManager.GetInstance().PlayHall(hallIndex);

    // }

    void SetTargetValue(AudioMod mod, float completion)
    {

        float currentValue = Extensions.mapRangeMinMax(0, 1, mod.range.start, mod.range.end, completion);

        switch (mod.modType)
        {
            case ModType.Reverb:
            case ModType.ParamEQ:
            case ModType.Distortion:

                mixer.SetFloat(mod.modType.ToString(), currentValue);
                break;

            case ModType.Chorus:

                // Special case for each of the 3 "Wet Mix" sliders
                for (int i = 1; i <= 3; i++)
                {
                    mixer.SetFloat("WetMix" + i, currentValue);
                }
                
                break;
        }
    }

}
