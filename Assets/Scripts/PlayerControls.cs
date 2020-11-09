using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerControls : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public float airControlStrength = 10F;
    public LayerMask groundCheckLayer;
    public float groundCheckRange = 0.1f;
    float moveDirection = 0;
    Rigidbody2D r2d;
    Collider2D mainCollider;

    ObjectGrabber grabber;

    GravityObject gravityObject;

    bool jumping = false;
    bool currGravityState= false;
    
    Rigidbody2D standingOn = null;
    Transform t;

    [HideInInspector]
    public bool facingRight = true; //determine which way the character is facing

    public Vector3 respawnPoint; //adding a respawn point to check where to spawn at a checkpoint

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        gravityObject = GetComponent<GravityObject>();
        grabber = GetComponent<ObjectGrabber>();

        respawnPoint = transform.position;
    }

    
    void Update()
    {
        // Movement controls
        //Current intention is that Update() captures all of the player intended inputs, FixedUpdate() then interprets them
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            moveDirection = 0;
        }
        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            jumping = true;
            standingOn = null;
        }

        if (Input.GetKeyDown("f"))
        {
             this.gravityObject.SwitchLocalGravity();
             Flip();
        }
    }

    void FixedUpdate()
    {
        // Apply movement velocity
        //Apply full speed in desired direction if grounded
        if (IsGrounded()){
            r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
        }
        //Else if we arent grounded, add force in the desired direction
        else{
            r2d.AddForce(new Vector2(airControlStrength*moveDirection, 0));
        }
        //Limit horizontal speed to max speed
        if(Mathf.Abs(r2d.velocity.x)>maxSpeed){
            r2d.velocity = new Vector2((maxSpeed*Mathf.Sign(r2d.velocity.x)), r2d.velocity.y);
        }

        //Update players facing (before factoring movement of object that the player is standing on)
        UpdateOrientation();

        //Add jump force if needed
        if (jumping){
            //Jump goes negative if gravity on player is reversed
            if (gravityObject.GetGravityState()){
                r2d.velocity = new Vector2(r2d.velocity.x, -jumpHeight);
            }
            else {
                r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            }
            jumping = false;
        }

        //Update our final speed with the speed of the object we're standing on if required
        if(IsGrounded()){
            //Only update if there is a significant amount of movement (hack to avoid weird twitching when standing on rigidbodies)
            if (Mathf.Abs(standingOn.velocity.x) > 0.01f){
                r2d.velocity = new Vector2 ( r2d.velocity.x + standingOn.velocity.x, r2d.velocity.y);
            }
        }
        
    }

    void UpdateOrientation(){
        //Rotate if players gravity has switched and update local gravity state
        if (currGravityState!=gravityObject.GetGravityState()){
            transform.Rotate(Vector3.forward * 180);
            //Added flip if velocity is greater than 0, to correct facing after inversion
            if (Mathf.Abs(r2d.velocity.x)>0){
                Flip();
            }
            currGravityState = gravityObject.GetGravityState();
        }
        //Update facing, but only if velocity is greater than 0 (standing still should change nothing)
        if (Mathf.Abs(r2d.velocity.x)>0){
            //Facing under normal gravity
            if(!currGravityState){
                // If the input is moving the player right and the player is facing left...
                if (Mathf.Sign(r2d.velocity.x) > 0 && !facingRight){
                    Flip();
                    Debug.Log("Player velocity is: " + r2d.velocity.x +" so triggering Flip 1");
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (Mathf.Sign(r2d.velocity.x) < 0 && facingRight){
                    Flip();
                    
                }
            }
            //Facing under inverted gravity
            else{
                // If the input is moving the player right and the player is facing left...
                if (Mathf.Sign(r2d.velocity.x) < 0 && !facingRight)
                    Flip();
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (Mathf.Sign(r2d.velocity.x) > 0 && facingRight)
                    Flip();
            }
        }
    }

    void Flip()
    {
        //Disallow flipping if the player is currently grabbing something
        if (!grabber.grabbing){
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private bool IsGrounded(){
        bool localIsGrounded = false;
        Bounds colliderBounds = mainCollider.bounds;
        Vector2 feetPosition = new Vector2();
        
        //Set foot position to either top or bottom of character depending on gravity direction
        if (!gravityObject.GetGravityState()){
            feetPosition = new Vector2(colliderBounds.center.x, colliderBounds.min.y);
        }
        else{
            feetPosition = new Vector2(colliderBounds.center.x, colliderBounds.max.y);
        }
        localIsGrounded = Physics2D.OverlapCircle(feetPosition, groundCheckRange, groundCheckLayer);

        //Grab the RigidBody were standing on if it exists
        if (localIsGrounded){
            standingOn = Physics2D.OverlapCircle(feetPosition, groundCheckRange, groundCheckLayer).gameObject.GetComponent<Rigidbody2D>();
        }

        //Debug by drawing the line were casting to check the ground
        //Debug.DrawLine(feetPosition, new Vector2(feetPosition.x, feetPosition.y - groundCheckRange), isGrounded ? Color.green : Color.red);
        return localIsGrounded;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        if (collision.tag == "CheckPoint")
        {
            respawnPoint = collision.transform.position;
        }
    }

}