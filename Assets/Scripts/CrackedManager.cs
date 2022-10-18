using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Student name: Rikveet singh hayer
 * Student id: 6590327
 */
public class CrackedManager : MonoBehaviour
{
    /*
     * This class is responsible for the crack simulation and is linked to the cracked object. It load the destroyed object's material(skin) and then applies an random explosion force from center. 
     */
    public void Crack(Material m)
    {
        for (int i=0; i < transform.childCount; i++)
        {
            GameObject t = transform.GetChild(i).gameObject;
            t.GetComponent<MeshRenderer>().material = m;
            t.GetComponent<Rigidbody>().AddExplosionForce((int)Random.Range(1000,2000), t.transform.position, Random.Range(7, 9));
        }
    }
}
