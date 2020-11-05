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
    bool isGrounded = false;
    Rigidbody2D r2d;
    Collider2D mainCollider;

    GravityObject gravityObject;

    bool jumping = false;
    bool currGravityState= false;
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
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            jumping = true;
        }

        if (Input.GetKeyDown("f"))
        {
             this.gravityObject.SwitchLocalGravity();
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

        UpdateOrientation();
    }

    void UpdateOrientation(){
        //Rotate if players gravity has switched and pdate local gravity state
        if (currGravityState!=gravityObject.GetGravityState()){
            transform.Rotate(Vector3.forward * 180);
            currGravityState = gravityObject.GetGravityState();
        }
        //Facing under normal gravity
        if(!currGravityState){
            // If the input is moving the player right and the player is facing left...
            if (Mathf.Sign(r2d.velocity.x) > 0 && !facingRight)
                Flip();
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (Mathf.Sign(r2d.velocity.x) < 0 && facingRight)
                Flip();
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

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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

        isGrounded = localIsGrounded;
        //Debug by drawing the line were casting to check the ground
        //Debug.DrawLine(feetPosition, new Vector2(feetPosition.x, feetPosition.y - groundCheckRange), isGrounded ? Color.green : Color.red);
        return isGrounded;
        
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