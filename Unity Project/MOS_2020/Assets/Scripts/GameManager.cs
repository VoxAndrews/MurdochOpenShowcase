using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
