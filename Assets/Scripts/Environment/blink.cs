using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
