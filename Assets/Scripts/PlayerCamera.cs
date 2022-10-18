using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Student name: Rikveet singh hayer
* Student id: 6590327.
*/

public class PlayerCamera : MonoBehaviour
{
    /*
    * This the class resposible for zooming out and in as the player size changes. 
    */
    public float senstivity;
    public GameObject player;
    Vector3 offset = Vector3.zero;
    private float speed;
    private Vector3 scale = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private void Start()
    {
        scale = player.transform.localScale;
        senstivity = 1;
        speed = 0.3F;
    }
    // if the player size is bigger than the current size, zoom out and set current size as player size.
    void Update()
    {
        if (player.transform.localScale.x>scale.x)
        {
            offset = player.transform.localScale -scale;
            scale = player.transform.localScale;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y + offset.y, transform.position.z - offset.z), ref velocity, speed * Time.deltaTime);
        }
    }
   
}
