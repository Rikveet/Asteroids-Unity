using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/*
 * Student name: rikveet singh hayer
 * Student id: 6590327
 */
public class world : MonoBehaviour
{
    /*
     * This class is responsible for the game events such as destuction and spawing of food and enemy ai, increase the dificulty, background music, record load and save user scores.
     */
    public GameObject food; // food prefab that player can eat
    public GameObject cracked; // "destroyed" state of a ball as both food and enemy ai are in shape of a ball.
    public GameObject enemy; // enemy ai prefab
    public GameObject player; // player object (space ship)
    public Material[] foodMaterials = new Material[5]; // food skins.
    public float respawnTime = 1;
    float xMax, xMin, zMax, zMin; // max and min range on x and z plane for spawing food and enemy ai.
    public float maxFood; // max amount of possible food.
    public float maxEnemy; // max amount of possible enemies at a given time.
    public int score; // current score of the player
    public int maxScore; // Saved score/ High score of a player
    public int Mass_Collected; // Total food eaten.
    public int Mass; // current size of the player.
    public TextMeshProUGUI mass; // text objects on the ui to player data
    public TextMeshProUGUI massCollected; 
    public TextMeshProUGUI scTxt;
    public TextMeshProUGUI highTxt;
    public GameObject pauseScreen; // A pause menu which becomes visible once escape is pressed.
    public GameObject[] backgroundMusic = new GameObject[5]; // Stores 5 different music audio sources. 
    public Slider sfx; // slider to record sfx sound level
    public Slider music;
    AudioSource current = null; // Current background music being played
    
    void Start() // construct
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Food"); // remove all the existing food objects
        for(int i =0; i< objects.Length; i++)
        {
            Destroy(objects[i]); 
        }
        objects = GameObject.FindGameObjectsWithTag("Enemy"); // remove all the enemies 
        for(int i =0; i< objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        Mass = 100; // current mass
        Mass_Collected = 0; // total mass collected
        score = 0; // enemies killed

        player.transform.localScale = Vector3.one; // player's size and position
        player.transform.position = new Vector3(0, 0.5F, 0);
        PlayerData d = SaveData.LoadPlayer(); // loading player's highscore.
        if (d != null)
        {
            maxScore = d.maxScore;
            highTxt.text = maxScore.ToString();
        }
        else
        {
            SaveData.SavePlayer(this);
            highTxt.text = "0";
        }
        musicPlayer();
        sfx.value = BackgroundData.sfxVol; // set the bars relative to value stored in the static class.
        music.value = BackgroundData.musicVol;
    }

    private void musicPlayer() // chooses a random soundtrack and plays on current music volume.
    {
        current = backgroundMusic[(int)Random.Range(0, backgroundMusic.Length)].GetComponent<AudioSource>();
        current.volume = BackgroundData.musicVol;
        current.Play();
    }
    private void spawnFood() // method tospawn food.
    {
        GameObject a = Instantiate(food) as GameObject;
        a.GetComponent<MeshRenderer>().material = foodMaterials[Mathf.RoundToInt(Random.Range(0, 5))]; // random skin from 0 to 5.
        float scale = Random.Range(player.transform.localScale.y, 2*player.transform.localScale.y); // random size from player's current size to 2* size.
        a.transform.localScale = new Vector3(scale, scale, scale);  // set scale
        a.transform.position = new Vector3(Random.Range(xMax+200,xMin-200), GameObject.FindGameObjectWithTag("Player").transform.position.y, Random.Range(zMax+200, zMin-200)); // random spawn point within the range. Within a 300x300 range except 200x200 range.
        a.SetActive(true); // show the object
    }
    private void spawnEnemy() // spawn enemy
    {
        GameObject a = Instantiate(enemy) as GameObject;
        float scale = Random.Range(player.transform.localScale.y/2, player.transform.localScale.y*2);
        a.transform.localScale = new Vector3(scale, scale, scale);
        a.transform.position = new Vector3(Random.Range(xMax-200, xMin+200), a.transform.localScale.y / 2, Random.Range(zMax-200, zMin+200));
        a.SetActive(true);
    }

    private void rangeCheck(GameObject[] objects) // despawn objects if they are not in range or fallen out of the map.
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (xMin > objects[i].transform.position.x || objects[i].transform.position.x > xMax || zMin > objects[i].transform.position.z || objects[i].transform.position.z > zMax || objects[i].transform.position.y< player.transform.position.y)
            {
                Destroy(objects[i]);
            }
        }
    }
   
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // game pause
        {
            BackgroundData.pause = !BackgroundData.pause;
        }
        while (GameObject.FindGameObjectsWithTag("Food").Length < maxFood) // if current number of food objects are less than the max amount spawn
        {
            spawnFood();
        }
        while (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemy)// if current number of enemies are less than max amount.
        {
            spawnEnemy();
        }
    }
    private void FixedUpdate()
    {
        if(BackgroundData.skin!=null && player.GetComponent<Renderer>().material != BackgroundData.skin) // if the current skin is not the selected skin
        {
            player.GetComponent<Renderer>().material = BackgroundData.skin;
        }
       GameObject p = GameObject.FindGameObjectWithTag("Player");
        Mass = (int)(p.transform.localScale.y * 1000); // update current player mass
        xMax = Mathf.Min(900, p.transform.position.x + 300); // updated the max and min range of spawning in x and z plane.
        xMin = Mathf.Max(-900, p.transform.position.x - 300);
        zMax = Mathf.Min(900, p.transform.position.z + 300);
        zMin = Mathf.Max(-900, p.transform.position.z - 300);
        rangeCheck(GameObject.FindGameObjectsWithTag("Food")); // despawn all the objects out of the range.
        rangeCheck(GameObject.FindGameObjectsWithTag("Enemy"));
        mass.text = Mass.ToString(); // update the text for current mass.
        if (maxScore < score)  // update high score
        {
            maxScore = score;
            highTxt.text = maxScore.ToString(); 
        }
        pauseScreen.SetActive(BackgroundData.pause); // if escape was pressed this will true, making the pause screen gui visible
        if (BackgroundData.pause) // lock/unlock mouse
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (GameObject.Find("PlayerBall").transform.localScale.y < 1F) // if scale is < 1 player dies.
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (maxScore < score)
            {
                SaveData.SavePlayer(this);
            }
            SceneManager.LoadScene(2);
        }
        current.volume = BackgroundData.musicVol; // current music's volume as player can change it in pause screen
        if (!current.isPlaying) // if music has stopped playing, start a new random one.
        {
            musicPlayer();
        }

    }
    public void addToMass(float m) // total mass update
    {
        Mass_Collected += (int)(m*1000);
        massCollected.text = Mass_Collected.ToString();
    }
    public void increaseScore() // increase score
    {
        score+=1;
        scTxt.text = score.ToString();
    }
    public void destroyObject(GameObject g, Material m) // replace current ball object with cracked ball and apply explosive force. Also play the explosion sound.
    {
            Vector3 position = g.transform.position;
            Quaternion rotation = g.transform.rotation;
            Vector3 scale = g.transform.localScale;
            Destroy(g);
            GameObject a = Instantiate(cracked, position, rotation) as GameObject;
            a.GetComponent<AudioSource>().volume = BackgroundData.sfxVol;
            a.GetComponent<AudioSource>().Play();
            a.transform.localScale = scale;
            a.GetComponent<CrackedManager>().Crack(m);
            Destroy(a, 7);     
    }
}
