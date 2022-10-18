using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Student name: Rikveet singh hayer
 * Student id: 6590327
 */
public class Ai : MonoBehaviour
{
    /*
     * This class is hooked to the enemy ai object, it is resposible for moving the enemy towards the player and reduce player size if collision takes place.
     */
    float speed;
    private Rigidbody rd;
    public GameObject world;
    void Start()
    {
        rd = gameObject.GetComponent<Rigidbody>();
        speed = Random.Range(1F,5F);
        world = GameObject.Find("Plane");
    }

    private void FixedUpdate()
    {
        if (!BackgroundData.pause)
        {
            GameObject player = GameObject.Find("PlayerBall");
            Vector3 direction = player.transform.position - transform.position; // if not paused keep on traveling towards the player with a constant random speed
            rd.velocity = (direction.normalized * speed);
        }
        else
        {
            rd.velocity = Vector3.zero;
        }

    }

    private void OnTriggerEnter(Collider collider) // the player collides with the enemy ai
    {
        if (collider.tag == "Player")
        {
            collider.transform.localScale = collider.transform.localScale - (collider.transform.localScale * 0.01F); //reduce the player's size
            world.GetComponent<world>().increaseScore(); // increase player's score
            world.GetComponent<world>().destroyObject(gameObject, gameObject.GetComponent<MeshRenderer>().material); // destroy the ai object.
            
        }
    }
}
