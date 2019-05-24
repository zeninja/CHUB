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

    public GameObject background;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (MetaSlider.GetInstance().stageInfo.world == 5 && MetaSlider.GetInstance().stageInfo.level == 4)
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
        // Debug.Log("STARTING VIDEO");
        AudioManager.GetInstance().FadeAllAudio();


        vp.enabled = true;
        // Play video
        vp.Play();
        background.SetActive(true);

        // Play audio
        manAudio.Play();
        womanAudio.Play();

        bgAudio1.Play();
        bgAudio2.Play();

        isPlaying = true;

        StartCoroutine(EndVideo());
    }

      IEnumerator EndVideo()
    {
        while (vp.isPlaying)
        {
            yield return null;
        }

        StartCoroutine(FadeBGAudio());
    }

    IEnumerator FadeBGAudio()
    {
        // yield return new WaitForSeconds(5);

        float t = 0;
        float d = 3;

        while (t < d)
        {
            t += Time.fixedDeltaTime;

            float p = t / d;

            bgAudio1.volume = 1 - EZEasings.SmoothStart2(p);
            bgAudio2.volume = 1 - EZEasings.SmoothStart2(p);

            yield return new WaitForFixedUpdate();
        }

        bgAudio1.Stop();
        bgAudio2.Stop();
    }

    // void LateUpdate()
    // {
    //     // if (testing) { return; }
    //     showVideo = false;
    //     vp.Stop();
    //     vp.enabled = false;
    // }
}
