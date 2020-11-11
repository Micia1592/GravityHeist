using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    private Scene scene;

    public  AudioSource menuMusic;
    public  AudioSource level4Music;
    public bool hasRun;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        hasRun = false;
        //menuMusic = GetComponent<AudioSource>();
        //level4Music = GetComponent<AudioSource>();

        menuMusic.loop = true;
        menuMusic.Play();

        level4Music.loop = false;
        //level4Music.Stop();
    }

    private void Update()
    {

        int scene = SceneManager.GetActiveScene().buildIndex;

        if (scene >= 3 && scene <= 6 && hasRun == false)
        {
           
            menuMusic.loop = false;
            menuMusic.Stop();

            level4Music.loop = true;
            level4Music.Play();
            hasRun = true;
        }

        else if (scene <= 7 && scene >= 9)
        {

            //play something ele
        }

    }
}
