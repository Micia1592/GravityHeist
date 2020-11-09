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
        if(collision.tag == "GravityItem")
        {
            Debug.Log("New object hit by grav gun");
            parentGravgun.InvertNewObject(collision.gameObject.GetComponent<GravityObject>());
            //collision.gameObject.GetComponent<GravityObject>().SwitchLocalGravity();

            //GetComponent<Rigidbody2D>().gravityScale *= -1;
            //transform.Rotate(Vector3.forward * 180);
            //gravityInverted = !gravityInverted;
            //Debug.Log("projectile Collided");

            Destroy(gameObject);
        }
        else if(collision.tag == "Solid")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Switch")
        {
            //collision.gameObject.GetComponent<GravityObject>().SwitchLocalGravity();
            parentGravgun.InvertNewObject(collision.gameObject.GetComponent<GravityObject>());
            Destroy(gameObject);
        }
        else if (collision.tag == "Spinnable")
        {

            Debug.Log("spinnable");
            collision.gameObject.GetComponent<SpinCupOnCollision>().canRotate = true;
            Destroy(gameObject); 

        }

        else if (collision.tag == "Wheel")
        {

            Debug.Log("Wheel");
            collision.gameObject.GetComponent<WheelSpin>().noGrav = !collision.gameObject.GetComponent<WheelSpin>().noGrav;
            Destroy(gameObject);

        }

    }

    public void SetParentGun(Gravgun parent){
        parentGravgun = parent;
    }

   
}
