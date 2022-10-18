using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Student name: Rikveet singh hayer
* Student id: 6590327
*/
public class projectileScript : MonoBehaviour
{
    public GameObject world;
    private Rigidbody rb;
    
    void Start() // get the rigid body component and the main script object.
    {
        rb = GetComponent<Rigidbody>();
        world = GameObject.Find("Plane");
    }

    void Update() // if the object is stuck destroy it
    {
        if(rb.velocity == Vector3.zero)
        {
            world.GetComponent<world>().destroyObject(gameObject, gameObject.GetComponent<MeshRenderer>().material);
        }
    }

    private void OnTriggerEnter(Collider collider) // if the enemy ball is inside the mesh i.e collided. Increase the player score and destroy the enemy object.
    { 
        if (collider.tag == "Enemy")
        {
            world.GetComponent<world>().increaseScore();
            world.GetComponent<world>().destroyObject(collider.gameObject, collider.gameObject.GetComponent<MeshRenderer>().material);
        }
    }
}
