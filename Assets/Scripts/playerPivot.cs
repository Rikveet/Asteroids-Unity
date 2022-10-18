using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Student name: Rikveet singh hayer
* Student id: 6590327
*/
public class playerPivot : MonoBehaviour
{
    /*
    * This class is reposible for looking around using camera. It's location is exactly the player's locations
    */
    public GameObject player; // the player obj
    public float senstivity; // sentivity "speed" of change
    private float speed; // speed at which the object follows the player
    private Vector3 scale = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        scale = player.transform.localScale;
        senstivity = 1;
        speed = 0.3F;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, speed * Time.deltaTime); // move to player's current locations.
    }
    private void LateUpdate() // handle camera rotaion using mouse input.
    {
        if (!BackgroundData.pause)
        {
            float rotateX = transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * senstivity; // the object is rotated on Y and X axis using it's current angles and the mouse input.
            float rotateY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * senstivity;
            if (rotateX < 0) // clamp the total rotation.
            {
                rotateX = 0;
            }
            if (rotateX > 60)
            {
                rotateX = 60;
            }
            transform.localEulerAngles = new Vector3(rotateX, rotateY, 0); // new rotation.
        }
    }
}
