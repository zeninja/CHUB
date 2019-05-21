using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackslideAudioController : MonoBehaviour
{

    AudioSource audioSource;
    public float maxVolume = 1;
    public Sound sound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // GiantSlider.OnBackslide += ProcessBackslideAudio;
    }

    void ProcessBackslideAudio(float amt) {
        Debug.Log("backsliding " + amt);
        audioSource.volume = EZEasings.SmoothStart2(amt);
    }
}
