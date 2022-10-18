using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Student name: Rikveet singh hayer
* Student id: 6590327
*/
public class skinHandler : MonoBehaviour
{
    /*
    * This class is reposible for handling the skin selection of the player, it is hooked to the material button and on click it updates the global static material to the current button's material.
    */
    public GameObject player;
    public void UpdateSkin()
    {
        player.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
        BackgroundData.skin = gameObject.GetComponent<Renderer>().material;
    }
}
