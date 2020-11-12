using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    
    public  AudioSource menuMusic;
    public  AudioSource level4Music;
    public AudioSource level7Music;
    public bool onLevel4;
    public bool onLevel7; 

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        onLevel4 = false;
        onLevel7 = false;

        menuMusic.loop = true;
        menuMusic.volume = 1;
        menuMusic.Play();

        level4Music.loop = false;
        level4Music.volume = 0;

        level7Music.loop = false;
        level7Music.volume = 0;
       
    }

    private void Update()
    {

        int scene = SceneManager.GetActiveScene().buildIndex;

        if (scene >= 5 && scene <= 8 && onLevel4 == false)
        {

            StartCoroutine(mainMenuTo4());
            level4Music.Play();
            level4Music.loop = true;
            onLevel4 = true;
        }

        else if (scene <= 9 && scene >= 11 && onLevel7  == false)
        {
            StartCoroutine(level7to9());
            level7Music.Play();
            level7Music.loop = true;
            onLevel7 = true;
        }
    }
        private IEnumerator mainMenuTo4()
        {
            var fadePerSec = menuMusic.volume / 2f;
           
            while (menuMusic.volume > 0)
            {
               menuMusic.volume = Mathf.MoveTowards(menuMusic.volume, 0, fadePerSec * Time.deltaTime);

                yield return null;
            }

        while (level4Music.volume < 1)
        {
            level4Music.volume = Mathf.MoveTowards(level4Music.volume, 1, fadePerSec * Time.deltaTime);
          
            yield return null;
        }

        }

    private IEnumerator level7to9()
    {
        var fadePerSec = level4Music.volume / 2f;

        while (level4Music.volume > 0)
        {
            level4Music.volume = Mathf.MoveTowards(level4Music.volume, 0, fadePerSec * Time.deltaTime);

            yield return null;
        }

        while (level7Music.volume < 1)
        {
            level7Music.volume = Mathf.MoveTowards(level7Music.volume, 1, fadePerSec * Time.deltaTime);

            yield return null;
        }

    }

}

