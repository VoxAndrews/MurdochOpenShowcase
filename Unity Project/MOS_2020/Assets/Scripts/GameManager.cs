﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject fpsObject;

    void Awake()
    {
        if(fpsObject == null)
        {
            fpsObject = GameObject.Find("FPSController");
        }

        HideAndLockCursor();
    }

    public void HideAndLockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisableInputs()
    {
        fpsObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().disableInput = true;
    }

    public void EnableInputs()
    {
        fpsObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().disableInput = false;
    }

    public void LoadScene(string name)
    {
        if(Application.CanStreamedLevelBeLoaded(name))
        {
            SceneManager.LoadScene(name);
        }
        else
            {
                Debug.Log("Error: Scene '" + name + "' not found! Reloading current scene");

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
    }
}
