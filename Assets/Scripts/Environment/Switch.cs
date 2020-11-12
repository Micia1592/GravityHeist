using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject switchObj;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Switch")
        {
            source.Play();
            Debug.Log("SWITCH");
            switchObj.SetActive(false);
        }
        //else 
        //{
        //    switchObj.SetActive(true);
        //}
        //if (collision.tag == "Player")
        //{
        //    switchObj.SetActive(false);
        //}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Switch")
        {
            source.Play();
            Debug.Log("SWITCH");
            switchObj.SetActive(true);
        }
    }

    
}
