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

    void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player")) {
            showVideo = true;
        }
    }

    void Update() {
        if(showVideo) {
            if(!isPlaying) {
                StartVideo();
            }
        }
    }

    public AudioSource manAudio;
    public AudioSource womanAudio;

    void StartVideo() {
        // Play video
        vp.Play();

        // Play audio
        manAudio  .Play();
        womanAudio.Play();

        isPlaying = true;
    }

    void LateUpdate()
    {
        if(testing) { return; }
        showVideo = false;
    }
}
