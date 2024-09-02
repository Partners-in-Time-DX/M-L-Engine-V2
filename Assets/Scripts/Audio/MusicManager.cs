using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup group;
    [SerializeField] private List<Music> allMusic;
    
    public static MusicManager instance;

    [HideInInspector] public Music musicPlaying = null;

    public static float currentPoint = 0;
    public bool DoneLoading;

    private string prevSongName = "";

    void Start()
    {
        DoneLoading = false;
        DontDestroyOnLoad(gameObject);
        if (Globals.MusicManager != null)
        {
            DoneLoading = true;
            Destroy(gameObject);
            return;
        }

        Globals.MusicManager = this;
        LoadSongs();
    }
    
    public void LoadSongs() {
        Dictionary<string, ArrayList> musicDict = new Dictionary<string, ArrayList>();
        TextAsset csvHandler = Addressables.LoadAssetAsync<TextAsset>("Assets/Audio/Music Data.csv").WaitForCompletion();

        if (csvHandler == null)
        {
            Debug.LogError("Failed to load music data");
            return;
        }
        
        musicDict = Globals.LoadCSV(csvHandler);

        allMusic = new List<Music>();

        int i = 0;
        foreach(KeyValuePair<string, ArrayList> entry in musicDict) {
            if (entry.Key == "" || entry.Key[0] == '#' || (string) entry.Value[1] == "Intro Point") continue;
            if (i != 0)
            {
                Music music = new Music();
                
                music.fileName = entry.Key;
                music.songName = Convert.ToString(entry.Value[0]);
                music.fileType = Convert.ToString(entry.Value[1]);
            
                try
                {
                    music.loopStart = Convert.ToSingle(entry.Value[2]);
                    music.loopEnd = Convert.ToSingle(entry.Value[3]);
                }
                catch
                {
                    music.redirect = Convert.ToString(entry.Value[4]);
                }

                if (music.redirect == null)
                {
                    String path = "Assets/Audio/Music/" + music.fileName + music.fileType;

                    AudioClip clipHandler = Addressables.LoadAssetAsync<AudioClip>(path).WaitForCompletion();

                    if (clipHandler != null)
                    {
                        music.source = gameObject.AddComponent<AudioSource>();
                        music.source.clip = clipHandler;
                        music.source.pitch = music.pitch;
                        music.source.loop = true;
                        music.source.outputAudioMixerGroup = group;
                    }
                    else
                    {
                        Debug.LogError("Failed to load song: " + music.fileName);
                    }
                }
            
                allMusic.Add(music);
            }

            i++;
        }
        
        DoneLoading = true;
    }

    public void setPoint()
    {
        currentPoint = musicPlaying.source.time;
    }

    public void goToPoint()
    {
        musicPlaying.source.time = currentPoint;
    }
    
    public void Stop(Music song=null)
    {
        if (song == null) song = musicPlaying;
        
        try { song?.source.Stop(); }
        catch {}

        if (song == musicPlaying) musicPlaying = null;
    }

    [Button]
    [EnableIf("CheckMusicListNotEmpty")]
    public Music PlayRandom()
    {
        System.Random rand = new System.Random();

        string s = allMusic.ElementAt(rand.Next(0, allMusic.Count)).fileName;

        return Play(s);
    }

    //Use with Play Random button
    public bool CheckMusicListNotEmpty() 
    {
        return allMusic?.Any() != false;
    }

    public Music Play(string name)
    {
        Music s = allMusic.Find(x => x.fileName == name);
        if (s == null) return null;

        if (!string.IsNullOrEmpty(s.redirect)) return Play(s.redirect);
        return Play(s);
    }
    
    public Music Play (Music s)
    {
        if (musicPlaying == s && Math.Abs(musicPlaying.source.volume - 1) < 0.1 && musicPlaying.source.isPlaying)
        {
            return musicPlaying;
        }

        try
        {
            if (musicPlaying.source.isPlaying)
            {
                FadeOut();
            }
        } catch {}
        
        if (prevSongName != s.fileName) currentPoint = 0f;
        
        musicPlaying = s;

        s.source.volume = 1;
        s.source.time = 0;
        s.source.Play();

        prevSongName = s.fileName;

        Debug.Log(s.fileName);
        return s;
    }

    private void FixedUpdate()
    {
        if (musicPlaying == null) return;
        if (musicPlaying.source == null) return;
        
        if (musicPlaying.fileName != "")
        {
            if (musicPlaying.source.time > musicPlaying.loopEnd && musicPlaying.loopEnd > 0)
            {
                musicPlaying.source.time -= (musicPlaying.loopEnd - musicPlaying.loopStart);
            }
        }
    }

    public void FadeOut(float length=0.1f)
    {
        try
        {
            setPoint();
        }
        catch
        {
            return;
        }

        StartCoroutine(FadeTo(length, 0, musicPlaying));
    }
    
    public void FadeIn(float length=0.1f)
    {
        musicPlaying.source.volume = 0f;
        musicPlaying.source.Play();
        goToPoint();
        StartCoroutine(FadeTo(length, 1, musicPlaying));
    }
    
    public IEnumerator FadeTo(float duration, float targetVolume, Music audioSource=null)
    {
        if (audioSource == null)
        {
            audioSource = musicPlaying;
        }
        
        float currentTime = 0;
        float start = audioSource.source.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        audioSource.source.volume = targetVolume;

        if (audioSource.source.volume <= 0.1)
        {
            Stop(audioSource);
        }
    }

    public void SetLowpass(float duration, float targetValue)
    {
        StartCoroutine(FadeLowpassFilter(duration, targetValue));
    }

    public IEnumerator FadeLowpassFilter(float duration, float targetValue)
    {
        float currentTime = 0;
        float start;
        group.audioMixer.GetFloat("Music Lowpass", out start);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            @group.audioMixer.SetFloat("Music Lowpass", Mathf.Lerp(start, targetValue, currentTime / duration));
            yield return null;
        }

        @group.audioMixer.SetFloat("Music Lowpass", targetValue);
    }

    public Music GetMusicPlaying() {
        return musicPlaying;
    }

    public void setMusicPlaying(Music music)
    {
        musicPlaying = music;
    }
}