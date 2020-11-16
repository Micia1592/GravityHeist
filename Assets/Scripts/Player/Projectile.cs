using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private GravityObject gravObj;
    private bool gravityInverted = false;

    private Gravgun parentGravgun;

    void Start()
    {
        Destroy(gameObject, 3);         //destroy the projectile after X number of seconds
      
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect what the grav gun has hit and do different things
        if(collision.tag == "GravityItem")
        {
            Debug.Log("New object hit by grav gun");
            parentGravgun.InvertNewObject(collision.gameObject.GetComponent<GravityObject>());

            parentGravgun.SetReadyToFire();


            Destroy(gameObject);
        }
        else if(collision.tag == "Solid")
        {
            parentGravgun.SetReadyToFire();
            Destroy(gameObject);
        }
        else if (collision.tag == "Switch")
        {
            parentGravgun.InvertNewObject(collision.gameObject.GetComponent<GravityObject>());
            parentGravgun.SetReadyToFire();
            Destroy(gameObject);
        }
        else if (collision.tag == "Spinnable")
        {

            Debug.Log("spinnable");
            collision.gameObject.GetComponent<SpinCupOnCollision>().canRotate = true;
            parentGravgun.SetReadyToFire();
            Destroy(gameObject); 

        }

        else if (collision.tag == "Wheel")
        {

            Debug.Log("Wheel");
            collision.gameObject.GetComponent<WheelSpin>().noGrav = !collision.gameObject.GetComponent<WheelSpin>().noGrav;
            parentGravgun.SetReadyToFire();
            Destroy(gameObject);

        }

    }

    public void SetParentGun(Gravgun parent){
        parentGravgun = parent;
    }

   
}
