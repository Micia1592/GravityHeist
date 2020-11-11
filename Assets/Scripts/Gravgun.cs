﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravgun : MonoBehaviour
{
    public Rigidbody2D projectile;  //put prefab of projectile here
    public float speed = 5f;       //speed of the projectile

    private PlayerControls playerCtrl;  //referencing the playercontrol script
    private Transform aimTransform;

    private GravityObject currentlyInvertedObject;
    private bool invertedActive = false;
    

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


        //Sprite Control
        Vector2 direction = new Vector2(mouseDirection.x - transform.position.x, mouseDirection.y - transform.position.y);
        this.transform.right = direction; 


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            //add audiosource here and/or animation
           
            mouseDirection = mouseDirection - transform.position;
            Rigidbody2D projectileInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            projectileInstance.velocity = new Vector2(this.transform.right.x * speed, this.transform.right.y* speed);
            projectileInstance.GetComponent<Projectile>().SetParentGun(this.GetComponent<Gravgun>());
        }

        //Drop object on right click
        if (Input.GetKeyUp(KeyCode.Mouse1)){
            currentlyInvertedObject.SwitchLocalGravity();
            currentlyInvertedObject = null;
            invertedActive = false;
        }
    }

    public void InvertNewObject(GravityObject gravObject){
        //Check if object is the same as current object, undo effect if so
        if (gravObject == currentlyInvertedObject){
            currentlyInvertedObject.SwitchLocalGravity();
            currentlyInvertedObject = null;
            invertedActive = false;
        }
        //Drop current object if present
        else if (invertedActive){
            currentlyInvertedObject.SwitchLocalGravity();
            currentlyInvertedObject = gravObject;
            currentlyInvertedObject.SwitchLocalGravity();
            invertedActive = true;
        }
        //If nothing currently activated
        else {
            currentlyInvertedObject = gravObject;
            currentlyInvertedObject.SwitchLocalGravity();
            invertedActive = true;
        }
    }
}
