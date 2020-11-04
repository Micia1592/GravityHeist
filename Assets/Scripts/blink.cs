using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour
{
    
    public GameObject blinkingObj;

    public float interval;

    private void Start()
    {
        InvokeRepeating("blinking", 0, interval);
    }

    void blinking ()
    {
        if (blinkingObj.activeSelf)
            blinkingObj.SetActive(false);
        else
            blinkingObj.SetActive(true);
    }
}
