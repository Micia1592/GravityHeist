using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{    
    public GameObject SceneSwitcher;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneSwitcher.GetComponent<SceneSwitcher>().SceneChange(); //Trigger scene change
        }
       
    }
}
