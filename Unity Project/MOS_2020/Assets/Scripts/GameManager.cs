using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject fpsObject;
    public GameObject canvas;

    Vector3 playerInitPos;

    [Range (0.0f, 100.0f)]
    public float rotationSpeedMultiplier;

    void Awake()
    {
        if(fpsObject == null)
        {
            fpsObject = GameObject.Find("FPSController");
        }

        if(canvas == null)
        {
            canvas = GameObject.Find("Canvas");
        }

        playerInitPos = fpsObject.transform.position;

        HideAndLockCursor();
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeedMultiplier);

        if(fpsObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().disableInput == false)
        {
            ManageInputs();
        }
    }

    public void ManageInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.GetComponent<CanvasManager>().ActivatePauseMenu();

            ShowAndConstrainCursor();

            DisableInputs();
        }
    }

    public void HideAndLockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowAndConstrainCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
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

    public void Respawn()
    {
        fpsObject.transform.position = playerInitPos;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");

        Application.Quit();
    }
}
