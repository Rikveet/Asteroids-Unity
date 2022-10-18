using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Student name: Rikveet singh hayer
 * Student id: 6590327
 */
public class MainMenu : MonoBehaviour
{   /*
     * This class is responsible for main menu button clicks
     */
    public void Play()
    {
        SceneManager.LoadScene(1); // start game
    }
    public void Options()
    {
        SceneManager.LoadScene(0); // load options
    }
    public void Quit()
    {
        Application.Quit(); // quit application
    }

    public void Back()
    {
        SceneManager.LoadScene(0); // go back to main menu
    }

    public void resume()
    {
        BackgroundData.pause = false; 
    }

}
