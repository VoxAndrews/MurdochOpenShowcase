﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using YoutubePlayer;

//This script should be attached to a VideoTrigger Object

public class VideoPlayback : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField]
    GameObject audioManage;

    [Header("Video Objects")]
    [SerializeField]
    GameObject youtubePlaybackObj; //The YoutubePlayer Prefab in the Scene, handles bringing YouTube videos into Unity, as well as Video Playback
    [SerializeField]
    GameObject videoScreenObj; //The Object whioch will be used as a screen
    [SerializeField]
    GameObject VideoPlaybackControlsObj;

    [Header("Materials & Textures")]
    [SerializeField]
    Material videoMaterial; //The Material which is used to show the Video (Has a Render Texture applied to it)
    [SerializeField]
    Material loadingMaterial; //The Material which is used when the Video is Loading
    [SerializeField]
    RenderTexture renderTex; //The Render Texture which the Video is pumped through

    [Header("Audio Playback")]
    [SerializeField]
    AudioSource playbackSpeaker; //The AudioSource that the Video's Audio will play through

    [Header("YouTube Settings")]
    [SerializeField]
    string webAddress; //The URL of the Video

    Material defaultMat; //The Material that the object is at in the beggining of the scene

    public bool triggerEntered = false;

    void Awake()
    {
        if(youtubePlaybackObj == null)
        {
            youtubePlaybackObj = GameObject.Find("YoutubePlayer");
        }

        youtubePlaybackObj.GetComponent<VideoPlayer>().audioOutputMode = VideoAudioOutputMode.Direct; //Sets the Video Audio Playback to Direct Audio by Default

        youtubePlaybackObj.GetComponent<YoutubePlayerScript>().youtubeUrl = "https://youtu.be/ekthcIHDt3I";

        renderTex.Release(); //Clears the Render Texture for Usage

        defaultMat = videoScreenObj.GetComponent<Renderer>().material; //Finds whatever the Material that is on the videoScreenObj amd sets it
    }

    void OnTriggerEnter(Collider collision) //Occurs when the User enters the VideoTrigger's Trigger
    {
        SetVideo(); //Runs the SetVideo() Function

        if(collision.gameObject.tag == "Player")
        {
            triggerEntered = true;
        }

        StartPlayback();
    }

    void OnTriggerExit(Collider collision) //Occurs when the User exits the VideoTrigger's Trigger
    {
        if (collision.gameObject.tag == "Player")
        {
            triggerEntered = false;
        }

        StopPlayback();
    }

    public void UnpuaseVideo()
    {
        if(triggerEntered == true)
        {
            youtubePlaybackObj.GetComponent<YoutubePlayerScript>().ResumeVideo();
        }
    }

    void SetVideo() //This sets the video for the specific 
    {
        youtubePlaybackObj.GetComponent<YoutubePlayerScript>().youtubeUrl = ""; //Clears the URL on the Youtube Player's 'YoutubePlayerScript' component

        youtubePlaybackObj.GetComponent<YoutubePlayerScript>().youtubeUrl = webAddress; //Sets the URL on the Youtube Player's 'YoutubePlayerScript' component to be equal to the webAddress String
    }

    async void StartPlayback()
    {
        videoScreenObj.GetComponent<Renderer>().material = loadingMaterial; //Changes the Video Screen to have the loadingMaterial applied

        await youtubePlaybackObj.GetComponent<YoutubePlayerScript>().PlayVideoAsync(); //Finds the Video from YouTube using the YoutubePlayerScript

        VideoPlaybackControlsObj.GetComponent<VideoPlaybackControls>().videoBackground.SetActive(false);

        VideoPlaybackControlsObj.GetComponent<VideoPlaybackControls>().enableFullscreen = false;

        renderTex.Create(); //Creates the new Render Texture with the Video

        videoScreenObj.GetComponent<Renderer>().material = videoMaterial; //Changes the Video Screen to have the videoMaterial applied

        if (playbackSpeaker != null) //Checks to see if playbackSpeaker has been set
        {
            youtubePlaybackObj.GetComponent<VideoPlayer>().SetTargetAudioSource(0, playbackSpeaker); //Sets the Video Player's AudioSource to be equal to playbackSpeaker

            if (youtubePlaybackObj.GetComponent<VideoPlayer>().audioOutputMode != VideoAudioOutputMode.AudioSource) //Checks to see if the Audio Mode is not set to AudioSource
            {
                Debug.Log("Setting Audio Playback to AudioSource");

                youtubePlaybackObj.GetComponent<VideoPlayer>().audioOutputMode = VideoAudioOutputMode.AudioSource; //Sets the Video Audio Playback to the Audio Source
            }
        }
        else
        {
            if (youtubePlaybackObj.GetComponent<VideoPlayer>().audioOutputMode != VideoAudioOutputMode.Direct)
            {
                Debug.Log(gameObject.name + " does not have an AudioSource, please attach one and set it");

                youtubePlaybackObj.GetComponent<VideoPlayer>().audioOutputMode = VideoAudioOutputMode.Direct; //Sets the Video Audio Playback to Direct Audio
            }
        }

        if (triggerEntered == true)
        {
            audioManage.GetComponent<AudioManager>().VideoStartup();

            youtubePlaybackObj.GetComponent<VideoPlayer>().Play(); //Plays the Video
        }
        else
        {
            StopPlayback();
        }
    }

    void StopPlayback()
    {
        videoScreenObj.GetComponent<Renderer>().material = defaultMat; //Sets the Object back to it's Default Material

        youtubePlaybackObj.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.RenderTexture; //Sets the video to render onto the Render Texture again

        youtubePlaybackObj.GetComponent<VideoPlayer>().audioOutputMode = VideoAudioOutputMode.Direct; //Sets the Video Audio Playback to Direct Audio

        youtubePlaybackObj.GetComponent<VideoPlayer>().Stop(); //Stops the video

        youtubePlaybackObj.GetComponent<VideoPlayer>().SetTargetAudioSource(0, null); //Clears the AudioSource on the Video Player

        youtubePlaybackObj.GetComponent<YoutubePlayerScript>().youtubeUrl = ""; //Clears the URL on the Youtube Player's 'YoutubePlayerScript' component

        renderTex.Release(); //Clears the Render Texture for Usage

        VideoPlaybackControlsObj.GetComponent<VideoPlaybackControls>().videoBackground.SetActive(false);

        VideoPlaybackControlsObj.GetComponent<VideoPlaybackControls>().enableFullscreen = false;
    }

    void EnterFullScreen() //This lets the video enter Fullscreen Mode (Currently Unused)
    {
        youtubePlaybackObj.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.CameraNearPlane; //Sets the Render Mode of the video to use the Camera Near-Plane
    }

    void ExitFullScreen() //This lets the video exit Fullscreen Mode (Currently Unused)
    {
        youtubePlaybackObj.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.RenderTexture; //Sets the Render Mode of the video to use the Render Texture
    }
}
