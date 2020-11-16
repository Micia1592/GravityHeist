using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This skips the first level so that menu manager can't be run twice.

public class loadMusicManager : MonoBehaviour
{
   
    void Awake()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
