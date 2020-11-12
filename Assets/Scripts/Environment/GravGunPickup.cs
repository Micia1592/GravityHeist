using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravGunPickup : MonoBehaviour
{
    private GameStateController controller;

    private void Start() {

        controller = GameObject.FindObjectOfType<GameStateController>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("collided");

            controller.EquipGravGun(true);
            
            Destroy(gameObject);
        }
    }
}
