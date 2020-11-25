using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource[] allAudioSources;
    float sfxVolume = 0.5f;
    float videoVolume = 0.5f; 

    public AudioSource[] sfxObjects;
    public VideoPlayer videoAudio;

    public bool videosInScene;

    // Start is called before the first frame update
    void Awake()  
    {
        allAudioSources = FindObjectsOfType<AudioSource>();

        Debug.Log("Total Audio Sources found: " + allAudioSources.Length);

        if(videosInScene == true)
        {
            if (videoAudio.audioOutputMode == VideoAudioOutputMode.AudioSource)
            {
                videoAudio.GetTargetAudioSource(0).volume = videoVolume;
            }
            else
                videoAudio.SetDirectAudioVolume(0, videoVolume);
        }

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

    public void ChangeVideoVolume(float volume)
    {
        videoVolume = volume;

        if (videoAudio.audioOutputMode == VideoAudioOutputMode.AudioSource)
        {
            videoAudio.GetTargetAudioSource(0).volume = videoVolume;
        }
        else
            videoAudio.SetDirectAudioVolume(0, videoVolume);
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
