using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravGunPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("collided");
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
