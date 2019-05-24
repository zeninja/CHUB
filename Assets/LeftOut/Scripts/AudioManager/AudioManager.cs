using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public static AudioManager GetInstance () {
        return instance;
    }

    public AudioMixer mixer;

    public AudioFader audioFader;

    public SoundSet[] hallSounds;
    public Sound[] cornerSounds;

    [System.Serializable]
    public class SoundSet {
        public string name;
        public int targetWorld;
        public Sound[] sounds;
        public AudioMixerGroup mixerGroup;
    }

    void Awake () {
        if (instance != null) {
            Destroy (gameObject);
        } else {
            instance = this;
            // DontDestroyOnLoad (gameObject);
        }

    }

    // void OnLevelWasLoaded () {

    // }

    void InitSounds () {
        List<Sound> fullSoundList = new List<Sound> ();

        foreach (SoundSet ss in hallSounds) {
            foreach (Sound s in ss.sounds) {
                s.source = gameObject.AddComponent<AudioSource> ();
                s.source.clip = s.clip;
                s.source.loop = s.loop;

                s.name = s.clip.name;
                s.source.playOnAwake = false;

                s.source.outputAudioMixerGroup = ss.mixerGroup;

                fullSoundList.Add (s);
            }
        }

        allSounds = fullSoundList.ToArray ();

    }

    void Start () {

        InitSounds ();

        // PlayNextHall();  // FIRST hall
        // MetaSlider.OnActiveSliderChanged += FadeOutAudio;

    }

    void OnEnable () {
        MetaSlider.OnActiveSliderChanged += PlayNextHall;
        MetaSlider.OnActiveSliderChanged += PlayNextCorner;
    }

    void OnDisable () {
        MetaSlider.OnActiveSliderChanged -= PlayNextHall;
        MetaSlider.OnActiveSliderChanged -= PlayNextCorner;
    }

    Sound[] allSounds;

    public void Play (string sound) {
        Sound s = Array.Find (allSounds, item => item.name == sound);
        if (s == null) {
            Debug.LogWarning ("Sound: " + sound + " not found!");
            return;
        }

        if (s.source == null) {
            return;
        }

        s.source.Play ();

        audioFader.SetLastSource (s.source);
    }

    public void PlayNextHall () {

        int world = MetaSlider.GetInstance ().stageInfo.world;
        int level = MetaSlider.GetInstance ().stageInfo.level;

        Play ("LEFT OUT_hallway" + world + "." + level);
    }

    public void PlayNextCorner() {
        if(MetaSlider.GetInstance().stageInfo.world == 1 && MetaSlider.GetInstance().stageInfo.level == 1) { return; }

        PlayAudioAtPoint(MetaSlider.GetInstance().GetCornerPos() + Vector3.up * 2);
    }

    public AudioSource cornerAudioSource;

    void PlayAudioAtPoint(Vector3 pt) {

        cornerAudioSource.Stop();
        cornerAudioSource.transform.position = pt;
        cornerAudioSource.clip = cornerSounds[UnityEngine.Random.Range(0, cornerSounds.Length)].clip;
        cornerAudioSource.Play();
        StartCoroutine(FadeCornerAudio());

        // AudioSource.PlayClipAtPoint(cornerSounds[UnityEngine.Random.Range(0, cornerSounds.Length)].clip, pt);
    }

    IEnumerator<WaitForFixedUpdate> FadeCornerAudio() {
        float t = 0;
        float d = 3;

        while (t < d) {
            t += Time.fixedDeltaTime;
            float p = t / d;
            cornerAudioSource.volume = 1 - EZEasings.SmoothStart3 (p);
            yield return new WaitForFixedUpdate ();
        }
    }

    public void FadeAllAudio () {
        StartCoroutine (FadeMaster ());
    }

    IEnumerator<WaitForFixedUpdate> FadeMaster () {
        float t = 0;
        float d = 1;

        while (t < d) {
            t += Time.fixedDeltaTime;
            float p = t / d;

            mixer.SetFloat ("MasterVolume", 0 - EZEasings.SmoothStop3 (p) * 80);
            yield return new WaitForFixedUpdate ();
        }
    }
}