using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravGunPickup : MonoBehaviour
{

    public GameObject gravGun;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("collided");

            if (gravGun!=null){
                gravGun.SetActive(true);
            }
            else{
                Debug.Log("GravGunPickup - Unable to find grav gun to activate it");
            }
            Destroy(gameObject);
        }
    }
}
