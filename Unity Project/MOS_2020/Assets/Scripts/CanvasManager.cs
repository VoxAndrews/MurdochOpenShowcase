using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject gameManage;
    public GameObject fadeInBlackWhite;
    public bool startup = false;

    void Awake() 
    {
        if (gameManage == null)
        {
            gameManage = GameObject.Find("GameManager");
        }

        if (fadeInBlackWhite == null)
        {
            fadeInBlackWhite = GameObject.Find("fadeIn_blackWhiteClear");
        }

        if(startup == true)
        {
            StartCoroutine(FadeInBlackToWhite());
        }
    }

    IEnumerator FadeInBlackToWhite() 
    {
        Debug.Log("Disabling User Input");
        gameManage.GetComponent<GameManager>().DisableInputs();

        Debug.Log("Started Coroutine at timestamp: " + Time.time);
        Debug.Log("Fading in from Black to White");

        fadeInBlackWhite.GetComponent<Animator>().Play("fadeIn_blackWhiteClear");

        // Yields for the length of the animation clip
        yield return new WaitForSeconds(2.5f);

        Debug.Log("Finished Coroutine at timestamp: " + Time.time);

        Debug.Log("Enabling User Input");
        gameManage.GetComponent<GameManager>().EnableInputs();
    }
}
