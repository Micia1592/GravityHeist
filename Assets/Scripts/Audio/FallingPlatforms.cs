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
       yPos = this.transform.position.y;
       yPos = Mathf.Clamp(yPos, -1, 1);
       source.pitch = yPos;
    }
}
