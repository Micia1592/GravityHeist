using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravgun : MonoBehaviour
{
    public Rigidbody2D projectile;  //put prefab of projectile here
    public float speed = 10f;       //speed of the projectile

    private PlayerControls playerCtrl;  //referencing the playercontrol script

    private void Awake()
    {
        playerCtrl = transform.root.GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the fire button is pressed
        if(Input.GetKeyDown("space"))
        {
            //add audiosource here and/or animation
            if(playerCtrl.facingRight)
            {
                Rigidbody2D projectileInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                projectileInstance.velocity = new Vector2(speed, 0);
            }
            else
            {
                Rigidbody2D projectileInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                projectileInstance.velocity = new Vector2(-speed, 0);
            }
        }
    }
}
