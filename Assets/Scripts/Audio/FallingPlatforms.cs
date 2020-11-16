using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    private AudioSource source;
    public float yPos;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
       yPos = this.transform.position.y; //this will change the pitch dependin on the height of the platform
       yPos = Mathf.Clamp(yPos, -1, 1); //contrain to these values
       source.pitch = yPos;
    }
}
