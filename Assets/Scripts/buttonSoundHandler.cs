using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Student name: Rikveet singh hayer
 * Student id: 6590327
 */
public class buttonSoundHandler : MonoBehaviour
{
    /*
     * This class is linked to buttons and upon hover or click it plays relative sound.
     */
    public GameObject click;
    public GameObject hover;
   public void buttonHover()
    {
        AudioSource h = click.GetComponent<AudioSource>();
        h.volume = BackgroundData.sfxVol;
        h.Play();
    }
   public void buttonClick()
    {
        AudioSource c = hover.GetComponent<AudioSource>();
        c.volume = BackgroundData.sfxVol;
        c.Play();
    }
}
