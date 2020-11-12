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
                

                //  showPaused();
            }
            else if (Time.timeScale == 0)
            {
                pauseCanvas.enabled = false;
                Time.timeScale = 1;
                // hidePaused();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseCanvas.enabled = false;
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
}
    
