using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackslideAudioController : MonoBehaviour {

    public AudioSource audioSource;
    // public float maxVolume = 1;
    // public Sound sound;

    bool fadeAudio;

    // Start is called before the first frame update
    void Start () {

    }

    void OnEnable () {
        GiantSlider.OnBackslide += ProcessBackslideAudio;
        GiantSlider.OnValueChanged += FadeAudio;
    }

    void OnDisable () {

        GiantSlider.OnBackslide -= ProcessBackslideAudio;
        GiantSlider.OnValueChanged -= FadeAudio;
    }

    void ProcessBackslideAudio (float amt) {
        fadeAudio = false;

        // Debug.Log("backsliding " + amt);
        if (MetaSlider.GetInstance ().stageInfo.world > 1 && MetaSlider.GetInstance ().stageInfo.world < 5) {
            audioSource.volume = EZEasings.SmoothStart3 (amt);
        }
    }

    void FadeAudio () {
        // audioSource = GetComponent<AudioSource> ();

        fadeAudio = true;
        // Debug.Log("fading audio");
        audioSource.volume = 0;

        // t = 0;
    }

    // float t;

    public float smoothing = 10;

    void Update () {
        if (fadeAudio) {

            // audioSource.volume = Mathf.Lerp(audioSource.volume, 0, Time.deltaTime * smoothing);

            // if(audioSource.volume < .1f) {
            audioSource.volume = 0;
            // }
        }
    }
}