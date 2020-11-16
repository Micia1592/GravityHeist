using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quit the game on button press, only works when game is built

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }
}

