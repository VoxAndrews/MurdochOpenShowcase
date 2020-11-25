using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject gameManage; //The Game Managment Object (With the GameManager.cs Script attached)
    public GameObject audioManage;
    public GameObject videoManage;
    public GameObject fadeObject; //The Fade Object (Which controls the Fade In/Out Transitions)
    public GameObject pauseMenu;
    public bool startup = false; //This Boolean dictates which scene uses the initial startup transition
    public bool newScene = false; //This Boolean dictates which scene uses the transition animation
    public string nextScene; //The name of the next scene to load
    public bool videosInScene;

    void Awake() 
    {
        if (gameManage == null)
        {
            gameManage = GameObject.Find("GameManager");
        }

        if (audioManage == null)
        {
            audioManage = GameObject.Find("AudioManager");
        }

        if(videoManage == null)
        {
            videoManage = GameObject.Find("YoutubePlayer");
        }

        if (fadeObject == null)
        {
            fadeObject = GameObject.Find("fadeIn_blackWhiteClear");
        }

        if ((newScene == true) && (startup == true))
        {
            Debug.Log("Both Transitions are applied, please only apply one. Disabling Scene Transition");
        }
        else
            {
                if (startup == true)
                {
                    StartCoroutine(FadeInBlackToWhite());
                }

                if (newScene == true)
                {
                    StartCoroutine(FadeInFromWhite());
                }
            }
    }

    public void ActivatePauseMenu()
    {
        Debug.Log("Opening Pause Menu");

        audioManage.GetComponent<AudioManager>().PauseAllAudio();

        if(videosInScene == true)
        {
            videoManage.GetComponent<YoutubePlayer.YoutubePlayerScript>().PauseVideo();
        }

        pauseMenu.SetActive(true);
    }

    public IEnumerator FadeInBlackToWhite() //The initial transition when starting the program
    {
        Debug.Log("Disabling User Input");
        gameManage.GetComponent<GameManager>().DisableInputs();

        Debug.Log("Started Coroutine at timestamp: " + Time.time);
        Debug.Log("Fading in from Black to White");

        fadeObject.GetComponent<Animator>().Play("fadeIn_blackWhiteClear");

        // Yields for the length of the animation clip
        yield return new WaitForSeconds(2.5f);

        Debug.Log("Finished Coroutine at timestamp: " + Time.time);

        Debug.Log("Enabling User Input");
        gameManage.GetComponent<GameManager>().EnableInputs();
    }

    public IEnumerator FadeOutToWhiteTransition() //Used to Transition between Scenes
    {
        Debug.Log("Disabling User Input");
        gameManage.GetComponent<GameManager>().DisableInputs();

        Debug.Log("Started Coroutine at timestamp: " + Time.time);
        Debug.Log("Fading out to White");

        fadeObject.GetComponent<Animator>().Play("fadeOut_clearWhite");

        yield return new WaitForSeconds(2f);

        Debug.Log("Finished Coroutine at timestamp: " + Time.time);

        Debug.Log("Loading '" + nextScene + "' Scene File");

        gameManage.GetComponent<GameManager>().LoadScene(nextScene);
    }

    public IEnumerator FadeInFromWhite() //The initial transition for a new scene
    {
        Debug.Log("Disabling User Input");
        gameManage.GetComponent<GameManager>().DisableInputs();

        Debug.Log("Started Coroutine at timestamp: " + Time.time);
        Debug.Log("Fading in from White");

        fadeObject.GetComponent<Animator>().Play("fadeIn_white");

        yield return new WaitForSeconds(2f);

        Debug.Log("Finished Coroutine at timestamp: " + Time.time);

        Debug.Log("Enabling User Input");
        gameManage.GetComponent<GameManager>().EnableInputs();
    }
}
