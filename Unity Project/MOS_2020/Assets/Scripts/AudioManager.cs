using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource[] allAudioSources;
    float sfxVolume = 0.5f;

    public AudioSource[] sfxObjects;
    public AudioSource videoAudio;

    // Start is called before the first frame update
    void Awake()  
    {
        allAudioSources = FindObjectsOfType<AudioSource>();

        Debug.Log("Total Audio Sources found: " + allAudioSources.Length);

        foreach (AudioSource audio in sfxObjects)
        {
            audio.volume = sfxVolume;
        }
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxVolume = volume;

        foreach (AudioSource audio in sfxObjects)
        {
            audio.volume = sfxVolume;
        }
    }

    public void PauseAllAudio()
    {
        foreach(AudioSource audio in allAudioSources)
        {
            audio.Pause();
        }
    }

    public void ResumeAllAudio() 
    {
        foreach (AudioSource audio in allAudioSources)
        {
            audio.UnPause();
        }
    }
}
