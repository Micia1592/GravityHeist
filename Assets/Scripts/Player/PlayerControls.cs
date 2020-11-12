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
    public float noInputBreakForce = 0.1f;
    public LayerMask groundCheckLayer;
    public float groundCheckRange = 0.1f;

    [SerializeField] private bool gravityFlipAllowed = false;
    float moveDirection = 0;
    Rigidbody2D r2d;

    Animator animator;
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
        animator = GetComponent<Animator>();

        respawnPoint = transform.position;
    }

    
    void Update()
    {


        //Update the players animator
        if (IsGrounded()){
            animator.SetBool("Jumping", false);
        }
        animator.SetFloat("MoveSpeed", Mathf.Abs(r2d.velocity.x));
        
        if (gravityFlipAllowed){

            if (Input.GetKeyDown("f"))
            {
                this.gravityObject.SwitchLocalGravity();
                Flip();
            }
        }
    }

    //Fixed update is where all movement inputs are accepted, and the forces on the players rigidbody are updated
    void FixedUpdate()
    {
         // Jumping
        if (Input.GetAxis("Jump")>0 && IsGrounded())
        {
            jumping = true;
            standingOn = null;
        }
        // Apply movement velocity
        //Apply full speed in desired direction if grounded
        if (IsGrounded()){
            r2d.velocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, r2d.velocity.y);
        }
        //Else if we arent grounded, add aircontrol force in desired direction
        else{
            r2d.AddForce(new Vector2(Input.GetAxis("Horizontal")*airControlStrength, 0));
        }
        //Cap horizontal speed to max speed
        if(Mathf.Abs(r2d.velocity.x)>maxSpeed){
            r2d.velocity = new Vector2((maxSpeed*Mathf.Sign(r2d.velocity.x)), r2d.velocity.y);
        }

        //Add jump force if jump has been triggered
        if (jumping){
            //Update the animator
            animator.SetBool("Jumping", true);
            //Jump goes negative if gravity on player is reversed
            if (gravityObject.GetGravityState()){
                r2d.velocity = new Vector2(r2d.velocity.x, -jumpHeight);
            }
            else {
                r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            }
            jumping = false;
        }

        //Add a constant break force if no horizontal movekeys are held and velocity greater than 1
        /*
        if (Input.GetAxis("Horizontal")==0&&r2d.velocity.x>noInputBreakForce){
            Debug.Log("Applying break force");
            float currXVelocity = Mathf.Abs(r2d.velocity.x);
            bool xVolPositive = r2d.velocity.x>0;
            if (xVolPositive){
                r2d.velocity = new Vector2(currXVelocity-noInputBreakForce, r2d.velocity.y);
            }
            else{
                r2d.velocity = new Vector2(currXVelocity+noInputBreakForce, r2d.velocity.y);
            }
            
        }
        */

        //Update players facing (before factoring movement of object that the player is standing on)
        UpdateOrientation();

        //Update our final speed with the speed of the object we're standing on if required
        if(IsGrounded()&&standingOn!=null){
            //Only update if there is a significant amount of movement (hack to avoid weird twitching when standing on rigidbodies)
            if (Mathf.Abs(standingOn.velocity.x) > 0.01f){
                r2d.velocity = new Vector2 ( r2d.velocity.x + standingOn.velocity.x, r2d.velocity.y);
            }
        }
        //Check for horizontal collisions with walls
        //HorizontalColChecker();
        
    }

    void UpdateOrientation(){
        //Rotate if players gravity has switched and update local gravity state
        if (currGravityState!=gravityObject.GetGravityState()){
            transform.Rotate(Vector3.forward * 180);
            Flip();
            currGravityState = gravityObject.GetGravityState();
        }
        //Update facing, but only if velocity is greater than 0 (standing still should change nothing)
        if (Mathf.Abs(r2d.velocity.x)>0){
            //Facing under normal gravity
            if(!currGravityState){
                // If the input is moving the player right and the player is facing left...
                if (Mathf.Sign(r2d.velocity.x) > 0 && !facingRight){
                    Flip();
                    //Debug.Log("Player velocity is: " + r2d.velocity.x +" so triggering Flip 1");
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
        //Disallow flipping if the player is currently grabbing something - Not now needed
        //if (!grabber.grabbing){
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        //}
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
            if (Physics2D.OverlapCircle(feetPosition, groundCheckRange, groundCheckLayer).gameObject.GetComponent<Rigidbody2D>() != null){
                standingOn = Physics2D.OverlapCircle(feetPosition, groundCheckRange, groundCheckLayer).gameObject.GetComponent<Rigidbody2D>();
            }
        }

        //Debug by drawing the line were casting to check the ground
        //Debug.DrawLine(feetPosition, new Vector2(feetPosition.x, feetPosition.y - groundCheckRange), isGrounded ? Color.green : Color.red);
        return localIsGrounded;
        
    }

    //Detects if we have just colided with a wall, sets horizontal velocity to 0 if so
    //Currently not being used at all. The issue this code was focused on is fixed with 
    private void HorizontalColChecker() {
        
        //Debug.Log("Checking for horizontal collisions");
        //float colDetectRange = Mathf.Abs(r2d.velocity.x);
        float colDetectRange = 0.23f;
        RaycastHit2D wall;
        if (gravityObject.GetGravityState()){
            wall = Physics2D.Raycast(transform.position, new Vector2(transform.position.x - (1*Mathf.Sign(transform.localScale.x)), 0), colDetectRange,groundCheckLayer);
            Debug.DrawLine(transform.position, new Vector2(transform.position.x - (colDetectRange*Mathf.Sign(transform.localScale.x)), transform.position.y), Color.red);
        }
        else{
            wall = Physics2D.Raycast(transform.position, new Vector2(transform.position.x + (1*Mathf.Sign(transform.localScale.x)), 0), colDetectRange,groundCheckLayer);
            Debug.DrawLine(transform.position, new Vector2(transform.position.x + (colDetectRange*Mathf.Sign(transform.localScale.x)), transform.position.y), Color.red);
        }

        if (wall.transform !=null){
            Debug.Log("We hit a wall, setting x velocity to 0");
            r2d.velocity = new Vector2(0, r2d.velocity.y);
        }

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