using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravGunPickup : MonoBehaviour
{
    private GameStateController controller;
    private AudioSource smash;
    private void Start() {

        controller = GameObject.FindObjectOfType<GameStateController>();
        smash = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("collided");
            smash.Play();
            controller.EquipGravGun(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 1.3f);
        }
    }
}
