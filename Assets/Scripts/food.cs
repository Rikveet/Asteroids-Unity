using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Student name: Rikveet singh hayer
 * Student id: 6590327
 */
public class food : MonoBehaviour
{
    /*
     * This class is linked to the food object and is responsible for it's collision and exsistence as food decomposess over time and handles increase in player size or ai's size upon collision.
     */
    public float score;
    public float age;
    public GameObject world;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        score = gameObject.transform.localScale.y;
        age = Random.Range(10000, 100000);
        world = GameObject.Find("Plane");
    }

    void Update()
    {
        age--;
        if (age <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && collider.transform.localScale.y <= 10) // if player's size is less than max limit
        {
            float massSize = Random.Range(0.01F, 0.1F); // take the random amount of mass from the food and add it to the player
            collider.transform.localScale = Vector3.SmoothDamp(collider.transform.localScale, collider.transform.localScale + (massSize * gameObject.transform.localScale), ref velocity, 0.3F * Time.deltaTime); // smoothly increase size.
            world.GetComponent<world>().addToMass((massSize * gameObject.transform.localScale.y)*100); // increase overall mass
        }
        else
        {
            collider.transform.localScale = Vector3.SmoothDamp(collider.transform.localScale, collider.transform.localScale + (Random.Range(0.001F, 0.01F) * gameObject.transform.localScale), ref velocity, 0.3F * Time.deltaTime); // smoothly increase collider's size.
        }
        world.GetComponent<world>().destroyObject(gameObject, gameObject.GetComponent<MeshRenderer>().material); // destroy the food object.
       
    }
}
