using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Music {
    
    public string songName;
    public string fileName;
    public string fileType;
    
    public float volume = 1;
    public float pitch = 1;

    public AudioSource source;
    public float loopStart;
    public float loopEnd;
    public string redirect;
}
