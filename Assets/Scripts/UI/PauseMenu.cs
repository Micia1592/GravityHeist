using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
            else if (Time.timeScale == 0)
            {
                pauseCanvas.enabled = false;
                optionsCanvas.enabled = false;
                Time.timeScale = 1;
               
            }
        }

    }


    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
    }


    public void RestartLevel()
    {

        Time.timeScale = 1;
        transition.SetTrigger("Start");
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void Options()
    {
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = true;

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
    
