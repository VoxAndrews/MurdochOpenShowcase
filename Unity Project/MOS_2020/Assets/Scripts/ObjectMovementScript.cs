using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementScript : MonoBehaviour
{
    [SerializeField] //Allows for the variable to be accessable in the inspector
    [Range(0.0f, 1.0f)] //The Minimum and Maximum range the variable can go to in the inspector
    float spinSpeed; //The Speed at which the object will rotate around 
    [SerializeField] //Allows for the variable to be accessable in the inspector
    [Range(0.0f, 10.0f)] //The Minimum and Maximum range the variable can go to in the inspector
    float floatSpeed; //The speed at which the object will float up and down
    [SerializeField] //Allows for the variable to be accessable in the inspector
    [Range(0.0f, 1.0f)] //The Minimum and Maximum range the variable can go to in the inspector
    float floatHeight; //The Height at which the object will float to (Affects the lowest/highest points)

    float orig_y; //The Y-Position which the object is at on Start

    void Start()
    {
        orig_y = transform.position.y; //Finds the current Y-Position at Start
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, spinSpeed, 0.0f, Space.World); //This will spin the Cube by a certian speed every frame (Space.World lets it use the Global Axis)

        transform.position = new Vector3(transform.position.x, orig_y + (Mathf.Sin(Time.time * floatSpeed) * floatHeight), transform.position.z); //Moves the object in an Up and Down motion using Sin
    }
}
