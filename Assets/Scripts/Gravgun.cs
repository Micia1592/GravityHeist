using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravgun : MonoBehaviour
{
    public Rigidbody2D projectile;  //put prefab of projectile here
    public float speed = 10f;       //speed of the projectile

    private PlayerControls playerCtrl;  //referencing the playercontrol script
    private Transform aimTransform;
    

    private void Awake()
    {
        playerCtrl = transform.root.GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        //Where is the mouse 
        Vector3 mouseDirection;
        mouseDirection = Input.mousePosition;
        mouseDirection.z = 0.0f;
        mouseDirection = Camera.main.ScreenToWorldPoint(mouseDirection);
        mouseDirection = mouseDirection - transform.position;
        this.transform.right = mouseDirection - transform.position; //sprite direction
       
        //if the fire button is pressed
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

          

            //add audiosource here and/or animation
            //Shoot in that direction
            
            Rigidbody2D projectileInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            projectileInstance.velocity = new Vector2(mouseDirection.x * speed, mouseDirection.y* speed);
         
        }
    }
}
