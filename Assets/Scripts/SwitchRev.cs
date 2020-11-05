using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRev : MonoBehaviour
{
    public GameObject switchObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Switch")
        {
            Debug.Log("SWITCH");
            switchObj.SetActive(true);
        }
        else
        {
            switchObj.SetActive(false);
        }
    }
}
