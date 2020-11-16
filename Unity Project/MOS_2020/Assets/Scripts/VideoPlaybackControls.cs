using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class VideoPlaybackControls : MonoBehaviour
{
    [SerializeField]
    public GameObject videoBackground;

    public bool enableFullscreen = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            switch(enableFullscreen)
            {
                case false:
                    Debug.Log("Activating Video Background");

                    videoBackground.SetActive(true);

                    Debug.Log("Enabling Fullscreen Mode");

                    gameObject.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.CameraNearPlane;

                    enableFullscreen = true;

                    break;

                case true:
                    Debug.Log("Deactivating Video Background");

                    videoBackground.SetActive(false);

                    Debug.Log("Disabling Fullscreen Mode");

                    gameObject.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.RenderTexture;

                    enableFullscreen = false;

                    break;
            }
        }
    }
}
