using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAudio : MonoBehaviour
{
    public AudioSource source;

    private void OnCollisionEnter2D(Collision2D collision)
    {
            source.Play();
   
        
    }
}
