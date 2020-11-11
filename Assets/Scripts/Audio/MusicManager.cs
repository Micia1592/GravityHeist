using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    private Scene scene;

    public  AudioSource menuMusic;
    public  AudioSource level4Music;
    public AudioSource level6Music;
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

        level6Music.loop = false;
        level6Music.volume = 0;
        //level4Music.Stop();
    }

    private void Update()
    {

        int scene = SceneManager.GetActiveScene().buildIndex;

        if (scene >= 4 && scene <= 6 && onLevel4 == false)
        {

            StartCoroutine(mainMenuTo4());
            level4Music.Play();
            onLevel4 = true;
        }

        else if (scene <= 7 && scene >= 9 && onLevel7  == false)
        {
            
            onLevel7 = true;
            //play something else
        }
    }
        private IEnumerator mainMenuTo4()
        {
            var fadePerSec = menuMusic.volume / 2f;

            while (menuMusic.volume > 0)
            {
               menuMusic.volume = Mathf.MoveTowards(menuMusic.volume, 0, fadePerSec * Time.deltaTime);

                // yield says: Interupt the routine here, render this frame
                // and continue from here in the next frame
                // In other words: Coroutines are like small temporary Update methods
                yield return null;
            }

        while (level4Music.volume < 1)
        {
            level4Music.volume = Mathf.MoveTowards(level4Music.volume, 1, fadePerSec * Time.deltaTime);

            // yield says: Interupt the routine here, render this frame
            // and continue from here in the next frame
            // In other words: Coroutines are like small temporary Update methods
            yield return null;
        }

        // Now the volume is 0
        //Destroy(menuMusic.gameObject);
        
        }


    }

