using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Student name: Rikveet singh hayer
* Student id: 6590327
*/
public class rotate : MonoBehaviour
{
    /*
    * This class is used to rotate the options menu object.
    */
    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * 10,0));
    }
}
