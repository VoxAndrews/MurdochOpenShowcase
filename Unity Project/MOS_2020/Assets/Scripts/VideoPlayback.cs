using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayback : MonoBehaviour
{
    [SerializeField]
    GameObject renderObject;
    [SerializeField]
    Material videoMaterial;
    [SerializeField]
    AudioSource audioPlayback;
    [SerializeField]
    string webAddress;
    [SerializeField]
    VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(videoPlayer == null)
        {
            videoPlayer = gameObject.GetComponent<VideoPlayer>();
        }
        
        if(videoPlayer.renderMode != UnityEngine.Video.VideoRenderMode.RenderTexture)
        {
            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;
        }

        videoMaterial.SetTexture("_MainTex", videoPlayer.targetTexture);
        videoMaterial.SetTexture ("_EmissionMap", videoPlayer.targetTexture);

        renderObject.GetComponent<MeshRenderer>().material = videoMaterial;

        if(videoPlayer.audioOutputMode != UnityEngine.Video.VideoAudioOutputMode.AudioSource)
        {
            videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
        }

        videoPlayer.SetTargetAudioSource(0, audioPlayback);

        videoPlayer.url = webAddress;

        videoPlayer.Prepare();

        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
