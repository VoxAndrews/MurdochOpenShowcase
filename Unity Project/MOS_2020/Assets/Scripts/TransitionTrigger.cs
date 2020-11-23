using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    public GameObject canvasObj;
    
    // Start is called before the first frame update
    void Start()
    {
        if(canvasObj == null)
        {
            canvasObj = GameObject.Find("Canvas");
        }
    }

    void OnTriggerEnter(Collider collision) 
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Trigger Entered");

            StartCoroutine(canvasObj.GetComponent<CanvasManager>().FadeOutToWhiteTransition());
        }
    }
}
