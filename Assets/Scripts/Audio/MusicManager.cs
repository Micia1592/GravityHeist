using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    
    public  AudioSource menuMusic;
    public  AudioSource level6Music;
    public AudioSource level11Music;
    public AudioSource finalMusic;
    public bool onMainMenu;
    public bool onLevel6;
    public bool onLevel11;
    public bool onFinalLevels;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        onLevel6 = false;
        onLevel11 = false;
        onFinalLevels = false;


       

        level6Music.loop = false;
        level6Music.volume = 0;

        level11Music.loop = false;
        level11Music.volume = 0;

        finalMusic.loop = false;
        finalMusic.volume = 0;
       
    }

    private void Update()
    {

        int scene = SceneManager.GetActiveScene().buildIndex;

        if(scene == 1 && onMainMenu == false) //reset when player returns to menu
        {
            onMainMenu = true;
            menuMusic.loop = true;
            menuMusic.volume = 1;
            menuMusic.Play();

            level6Music.Stop();
            level11Music.Stop();
            finalMusic.Stop();

            onLevel11 = false;
            onLevel6 = false;
            onFinalLevels = false;
        }


        else if (scene == 6 &&  onLevel6 == false)
        {

            StartCoroutine(mainMenuTo6());
            level6Music.Play();
            level6Music.loop = true;
            onLevel6 = true;
            onMainMenu = false;
        }
        
        else if (scene == 11 && onLevel11  == false)
        {
           
            StartCoroutine(level11to15());
            level11Music.Play();
            level11Music.loop = true;
            onLevel11 = true;
            onLevel6 = false;
           
        }
        
        else if (scene == 16 && onFinalLevels == false)
        {
            StartCoroutine(level16toFinal());
            finalMusic.Play();
            finalMusic.loop = true;
            onFinalLevels = true;
            onLevel11 = false;
           
        }
    }
        private IEnumerator mainMenuTo6()
        {
            var fadePerSec = menuMusic.volume / 2f;
           
            while (menuMusic.volume > 0)
            {
               menuMusic.volume = Mathf.MoveTowards(menuMusic.volume, 0, fadePerSec * Time.deltaTime);

                yield return null;
            }

        while (level6Music.volume < 1)
        {
            level6Music.volume = Mathf.MoveTowards(level6Music.volume, 1, fadePerSec * Time.deltaTime);
          
            yield return null;
        }

        }

    private IEnumerator level11to15()
    {
        var fadePerSec = level6Music.volume / 2f;
        
        while (level6Music.volume > 0)
        {
            level6Music.volume = Mathf.MoveTowards(level6Music.volume, 0, fadePerSec * Time.deltaTime);

            yield return null;
        }

        while (level11Music.volume < 1)
        {
            level11Music.volume = Mathf.MoveTowards(level11Music.volume, 1, fadePerSec * Time.deltaTime);

            yield return null;
        }

    }

    private IEnumerator level16toFinal()
    {
        var fadePerSec = level6Music.volume / 2f;

        while (level6Music.volume > 0)
        {
            level6Music.volume = Mathf.MoveTowards(level6Music.volume, 0, fadePerSec * Time.deltaTime);

            yield return null;
        }

        while (finalMusic.volume < 1)
        {
            finalMusic.volume = Mathf.MoveTowards(finalMusic.volume, 1, fadePerSec * Time.deltaTime);

            yield return null;
        }

    }

}

