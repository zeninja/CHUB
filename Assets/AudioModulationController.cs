using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioModulationController : MonoBehaviour
{

    public enum ModType { delay, reverb, }

    [System.Serializable]
    public class AudioMod {
        public string name;
        public ModType modType;
        public Extensions.Property range;
    }

    [SerializeField]
    public List<AudioMod> audioMods;

}
