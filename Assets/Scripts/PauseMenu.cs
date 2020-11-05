using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{


    private bool isPaused;
    public Canvas pauseCanvas;

    void Start()
    {
        isPaused = false;
        pauseCanvas.enabled = false;
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
}
    
