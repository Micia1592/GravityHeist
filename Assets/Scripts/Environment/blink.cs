using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script blinks an object in and out, e.g. signs

public class blink : MonoBehaviour
{
    public GameObject blinkingObj;

    public float interval;

    public AudioSource source;
  

    private void Start()
    {
        InvokeRepeating("blinking", 0, interval);
        source.Play();
    }

    void blinking ()
    {
        if (blinkingObj.activeSelf)
        {
            blinkingObj.SetActive(false);
            source.volume = 0.1f;
        }
        else
        {
            blinkingObj.SetActive(true);
            source.volume = 0.2f;
        }
    }
}
