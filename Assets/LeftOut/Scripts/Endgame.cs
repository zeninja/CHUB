using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Endgame : MonoBehaviour
{

    public bool testing;
    public bool showVideo = false;
    bool isPlaying;

    public VideoPlayer vp;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (MetaSlider.GetInstance().stageInfo.world >= 5)
            {
                showVideo = true;
            }
        }
    }

    void Update()
    {
        if (showVideo)
        {
            if (!isPlaying)
            {
                StartVideo();
            }
        }
    }

    public AudioSource manAudio;
    public AudioSource womanAudio;

    public AudioSource bgAudio1;
    public AudioSource bgAudio2;

    void StartVideo()
    {
        AudioManager.GetInstance().FadeAllAudio();


        vp.enabled = true;
        // Play video
        vp.Play();

        // Play audio
        manAudio.Play();
        womanAudio.Play();

        bgAudio1.Play();
        bgAudio2.Play();

        isPlaying = true;
    }

    // void LateUpdate()
    // {
    //     // if (testing) { return; }
    //     showVideo = false;
    //     vp.Stop();
    //     vp.enabled = false;
    // }
}
