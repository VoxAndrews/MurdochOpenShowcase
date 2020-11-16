using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class VideoPlaybackControls : MonoBehaviour
{
    [SerializeField]
    public GameObject videoBackground;
    [SerializeField]
    GameObject youtubePlay; 

    [HideInInspector]
    public bool enableFullscreen = false;

    bool triggerEntered = false;

    void Update()
    {
        if(youtubePlay.GetComponent<VideoPlayer>().renderMode == VideoRenderMode.CameraNearPlane)
        {
            videoBackground.SetActive(true);
        }
        
        if ((Input.GetKeyDown(KeyCode.F)) && (triggerEntered == true))
        {
            switch (enableFullscreen)
            {
                case false:
                    Debug.Log("Activating Video Background");

                    videoBackground.SetActive(true);

                    Debug.Log("Enabling Fullscreen Mode");

                    youtubePlay.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.CameraNearPlane;

                    enableFullscreen = true;

                    break;

                case true:
                    Debug.Log("Deactivating Video Background");

                    videoBackground.SetActive(false);

                    Debug.Log("Disabling Fullscreen Mode");

                    youtubePlay.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.RenderTexture;

                    enableFullscreen = false;

                    break;
            }
        }

        if((triggerEntered == true) && (!Input.GetKeyDown(KeyCode.F)) && (Input.anyKeyDown))
        {
            Debug.Log("Deactivating Video Background");

            videoBackground.SetActive(false);

            Debug.Log("Disabling Fullscreen Mode");

            youtubePlay.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.RenderTexture;

            enableFullscreen = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Video Trigger")
        {
            triggerEntered = true;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Video Trigger")
        {
            triggerEntered = false;
        }
    }
}
