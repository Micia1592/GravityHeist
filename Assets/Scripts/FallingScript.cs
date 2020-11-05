using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingScript : MonoBehaviour
{
    public GameObject fallingPlatform;
    public float fallDelay = 1.0f;
    public float respawnTime = 2f;
    Rigidbody2D rb;
    Vector2 location; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        location = GetComponent<Transform>().position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    
     {
            if (collision.gameObject.tag == ("Player"))
            {
            Invoke("Fall", fallDelay);
            Debug.Log("player collided" + collision.gameObject.name);

            }
            else if (collision.gameObject.tag == ("Solid"))
        {
            //Invoke("Respawn", respawnTime);
            Destroy(gameObject);
        }
     }
    void Fall()
    {
        rb.isKinematic = false;
    }
    //void Respawn()
    //{
        
    //    Instantiate(fallingPlatform, location, new Quaternion(0,0,0,0));
    //    Debug.Log("platform spawned");

        
    //}

   

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
