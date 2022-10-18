using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Student name Rikveet singh hayer
* Student id: 6590327
*/
public class sfxVolHandler : MonoBehaviour
{
    /*
    * This class is responsible for static volume levels of music and sfx, it is hooked to the volume sliders which call relative methods on slide.
    */
    public static void updateSFX(float v)
    {
        BackgroundData.sfxVol = v;
    }

    public static void updateMusic(float v)
    {
        BackgroundData.musicVol = v;
    }
}
