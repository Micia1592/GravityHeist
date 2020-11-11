using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject switchObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Switch")
        {
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
            Debug.Log("SWITCH");
            switchObj.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
