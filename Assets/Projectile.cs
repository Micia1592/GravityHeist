using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private ReverseGrav reverseGrav;
    private bool gravityInverted = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);         //destroy the projectile after X number of seconds
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "GravityItem")
        {
            collision.gameObject.GetComponent<GravityEffected>().reverseGravity();

            //GetComponent<Rigidbody2D>().gravityScale *= -1;
            //transform.Rotate(Vector3.forward * 180);
            //gravityInverted = !gravityInverted;
            //Debug.Log("projectile Collided");

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
