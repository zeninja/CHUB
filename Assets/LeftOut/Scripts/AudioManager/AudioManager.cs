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

    public SoundSet[] hallSounds;
    public Sound[] otherSounds;

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
            DontDestroyOnLoad(gameObject);
        }

        InitSounds ();
    }

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
        // PlayNextHall();  // FIRST hall
        MetaSlider.OnActiveSliderChanged += FadeOutAudio;
        MetaSlider.OnActiveSliderChanged += PlayNextHall;
    }

    Sound[] allSounds;

    public void Play (string sound) {
        Sound s = Array.Find (allSounds, item => item.name == sound);
        if (s == null) {
            Debug.LogWarning ("Sound: " + sound + " not found!");

            return;
        }

        // s.source.volume = s.volume * (1f + UnityEngine.Random.Range (-s.volumeVariance / 2f, s.volumeVariance / 2f));
        // s.source.pitch  = s.pitch  * (1f + UnityEngine.Random.Range (-s.pitchVariance / 2f, s.pitchVariance / 2f));
        // s.source.playOnAwake = false;

        s.source.Play ();
        // Debug.Log("PLAAAYYYYY AUDIO " + s.source.clip);

        lastSource = s.source;

        // Debug.Log("Playing " + s.name);
    }

    public void PlayNextHall () {
        // Debug.Log("Playing hall");

        int world = MetaSlider.GetInstance ().stageInfo.world;
        int level = MetaSlider.GetInstance ().stageInfo.level;

        Play ("LEFT OUT_hallway" + world + "." + level);
    }

    public AudioSource lastSource;

    void FadeOutAudio() {
                // Debug.Log("FadeOutAudio ");
        if (lastSource != null) {
            StartCoroutine (FadeLastSource ());
        }
    }

    IEnumerator<WaitForFixedUpdate> FadeLastSource () {
        Debug.Log("FADING PREVIOUS SOURCE");

        float t = 0;
        float d = 1;

        if (lastSource != null) {
            while (t < d) {
                t += Time.fixedDeltaTime;
                float p = t / d;
                lastSource.volume = 1 - EZEasings.SmoothStop3 (p);
                yield return new WaitForFixedUpdate ();
            }
        } 
        else {
            yield return null;
        }

    }

    public void FadeAllAudio() {
        StartCoroutine(FadeMaster());
    }

    IEnumerator<WaitForFixedUpdate> FadeMaster() {
        float t = 0;
        float d = 1;

        while (t < d) {
            t += Time.fixedDeltaTime;
            float p = t / d;

            mixer.SetFloat ("MasterVolume", 0 - EZEasings.SmoothStop3 (p) * 80);
            yield return new WaitForFixedUpdate ();
        }
    }

    // void GetSounds()
    // {

    //     AudioClip[] hallSounds = (AudioClip[])Resources.LoadAll("HallwayAudio", typeof(AudioClip));
    //     AudioClip[] crnrSounds = (AudioClip[])Resources.LoadAll("CornerAudio", typeof(AudioClip));
    //     AudioClip[] miscSounds = (AudioClip[])Resources.LoadAll("OtherAudio", typeof(AudioClip));

    //     List<Sound> soundList = new List<Sound>();

    //     foreach (AudioClip c in hallSounds)
    //     {
    //         Sound newSound = new Sound();
    //         newSound.clip = c;

    //         soundList.Add(newSound);
    //     }

    //     foreach (AudioClip c in crnrSounds)
    //     {
    //         Sound newSound = new Sound();
    //         newSound.clip = c;

    //         soundList.Add(newSound);
    //     }

    //     foreach (AudioClip c in miscSounds)
    //     {
    //         Sound newSound = new Sound();
    //         newSound.clip = c;

    //         soundList.Add(newSound);
    //     }

    //     sounds = soundList.ToArray();
    // }

    // public void PlayCurrentHall() {

    // 	int world = MetaSlider.GetInstance().stageInfo.world;
    // 	int level = MetaSlider.GetInstance().stageInfo.level;

    // 	Play("Hallway " + world + "-" + level);
    // }

    // bool hasStarted;

    // public void HandleSliderEntered () {
    // 	if (!hasStarted) {
    // 		AudioManager.instance.Play ("LabyrinthStart");
    // 		hasStarted = true;

    // 		Invoke ("SwitchToDrone", 15);
    // 	}
    // }

    // void SwitchToDrone() {
    // 	Play("Drone");
    // }

    // public void PlayAtNextCorner() {
    // 	// audioSource
    // }

}