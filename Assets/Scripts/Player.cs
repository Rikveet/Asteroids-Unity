using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Student name: rikveet singh hayer
* Student id: 6590327
*/
public class Player : MonoBehaviour
{
    /*
    * this class is hooked to the player object and is responsible for player movement, projectile shooting and player size decomposition over time.
    */

    private Rigidbody rb;
    public GameObject cam; // camera looking at the pivot -> player.
    public GameObject miniMapCam; // mini map cam
    public GameObject bullet; // projectile
    public GameObject pivot; // linked camera
    public GameObject fire; // fire sound
    float speed = 5; // base speed.
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // this method is responsible for shooting projectiles
    private void Update()
    {
        if (!BackgroundData.pause)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) // if left shit is pressed
            {
                if (transform.localScale.x > 1) // size is greater than minimum mass
                {
                    GameObject b = Instantiate(bullet) as GameObject;
                    b.transform.position = transform.position + (transform.forward * gameObject.GetComponent<Renderer>().bounds.size.x / 2); // position the object infront of the player model
                    b.transform.localScale = Vector3.one; // size 1
                    Vector3 direction = transform.forward; // fire direction is player's forward vector.
                    b.GetComponent<Rigidbody>().velocity = (direction.normalized * 150); // fire only in the direction i.e ignoring the values at speed 150
                    Destroy(b, 3); // destroy the object in 3 seconds.
                    Vector3 newScale = transform.localScale - (transform.localScale * 0.01F); // remove 1% of projectile's size from the player's mass.
                    AudioSource f = fire.GetComponent<AudioSource>(); // play the fire sound
                    f.volume = BackgroundData.sfxVol;
                    f.Play();
                    if (newScale.x < 1 || newScale.y < 1 || newScale.z < 1) // if scale is smaller than min
                    {
                        transform.localScale = Vector3.one;
                    }
                    else
                    {
                        transform.localScale = newScale;
                    }
                }
            }
        }
    }

    // function responsible for constant decomposition of the player's extra mass and setting player's y rotation equal to the updated pivot's rotation which rotate according to the mouse. And player movement.
    private void FixedUpdate()
    {
        if (!BackgroundData.pause)
        {
            Vector3 direction = cam.transform.forward * Input.GetAxis("Vertical") + cam.transform.right * Input.GetAxis("Horizontal"); // get current direction taking in player input and camera's forward and right direction
            direction.y = 0; // make sure the player stays in the same plane.
            rb.velocity = (direction.normalized * (speed) / (gameObject.transform.localScale.y * 50 / 100)); // current velocity is dependent on the direction calcuated, base speed and player size.
            miniMapCam.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y * gameObject.transform.localScale.y) + 100, gameObject.transform.position.z); // mini maps zooms in or out depending on player's size and keep's player at center.
            if (transform.localScale.x > 1) // if extra mass reduce it.
            {
                transform.localScale = transform.localScale - (transform.localScale * 0.0001F);
                if (transform.localScale.x < 1 || transform.localScale.y < 1 || transform.localScale.z < 1)
                {
                    transform.localScale = Vector3.one;
                }
            }
            Quaternion q = pivot.transform.rotation; // set the player's current rotation to pivot's rotation.
            q.z = 0;
            q.x = 0;
            transform.rotation = q;
        }
        else
        {
            rb.velocity = Vector3.zero; // if pause stop the input.
        }
    }
}
