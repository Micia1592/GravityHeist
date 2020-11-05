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

    public LayerMask groundCheckLayer;
    public float groundCheckRange = 0.1f;
    float moveDirection = 0;
    bool isGrounded = false;
    Rigidbody2D r2d;
    Collider2D mainCollider;

    GravityObject gravityObject;
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

    // Update is called once per frame
    void Update()
    {
        //Debugging the grounded check
        if (isGrounded){
            //Debug.Log("Player is grounded");
        }
        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && isGrounded)
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }   
        }
        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            //Jump goes negative if gravity on player is reversed
            if (gravityObject.GetGravityState()){
                r2d.velocity = new Vector2(r2d.velocity.x, -jumpHeight);
            }
            else {
                r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            }

            
        }

        if (Input.GetKeyDown("f"))
        {
             this.gravityObject.SwitchLocalGravity();
        }
    }

    void FixedUpdate()
    {
        //Check current gravity on player to see if its changed. Flip their transform if it has
        if (gravityObject.GetGravityState()!=currGravityState){
            transform.Rotate(Vector3.forward * 180);
            currGravityState = gravityObject.GetGravityState();
            //Flip();
        }

        IsGrounded();

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        //anim.SetFloat("Speed", Mathf.Abs(h));

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            // ... add a force to the player.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);


        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();

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