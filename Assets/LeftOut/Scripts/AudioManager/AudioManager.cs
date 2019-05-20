using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;
	public static AudioManager GetInstance() {
		return instance;
	}

	public AudioMixerGroup mixerGroup;
	public AudioMixer mixer;

	public Sound[] sounds;

	void Awake () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}

		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			// s.source.outputAudioMixerGroup = mixerGroup;
			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}

	public void Play (string sound) {
		Sound s = Array.Find (sounds, item => item.name == sound);
		if (s == null) {
			Debug.LogWarning ("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range (-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch  = s.pitch  * (1f + UnityEngine.Random.Range (-s.pitchVariance  / 2f, s.pitchVariance / 2f));

		s.source.Play ();

		Debug.Log("Playing " + name);
	}

	public void PlayCurrentHall() {

		int world = MetaSlider.GetInstance().stageInfo.world;
		int level = MetaSlider.GetInstance().stageInfo.level;

		Debug.Log("triggering hall music " + world + "-" + level);

		Play("Hallway " + world + "-" + level);
	}
	
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

}