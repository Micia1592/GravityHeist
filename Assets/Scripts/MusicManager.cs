using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    private Scene scene;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); //keep music manager running 
    }

    private void Update()
    {

        int scene = SceneManager.GetActiveScene().buildIndex;

        if (scene > 3 && scene < 6)
        {
            //switch music
        }

    }
}
