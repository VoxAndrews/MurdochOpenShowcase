using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public GameObject gameManage;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManage == null)
        {
            gameManage = GameObject.Find("GameManager");
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Respawning Player");

            gameManage.GetComponent<GameManager>().Respawn();
        }
    }
}
