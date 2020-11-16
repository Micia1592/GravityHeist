using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//our pause menu handler. Each void is called when a particlar button is pressed while paused

public class PauseMenu : MonoBehaviour
{

    public Animator transition;

    private bool isPaused;
    public Canvas pauseCanvas;
    public Canvas optionsCanvas;

    void Start()
    {
        isPaused = false;
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;
    }

    void Update()
    {
     

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1) //If the game is running, pause
            {
                pauseCanvas.enabled = true;
                Time.timeScale = 0;
                

                
            }
            else if (Time.timeScale == 0) //if the game is already paused, unpause
            {
                pauseCanvas.enabled = false;
                optionsCanvas.enabled = false;
                Time.timeScale = 1;
               
            }
        }

    }


    public void ResumeGame()
    {
        Time.timeScale = 1;        //set game playable
        pauseCanvas.enabled = false;
    }


    public void RestartLevel()
    {

        Time.timeScale = 1;
        transition.SetTrigger("Start");  //Play our crossfade
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //load level again
        
    }

    public void Options()
    {
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = true; //launch th options canvas

    }


    public void BackButton()
    {
        optionsCanvas.enabled = false;
        pauseCanvas.enabled = true;
    }

    public void SkipLevel()
    {
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
    
