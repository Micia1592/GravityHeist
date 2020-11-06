using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{

    public bool grabbing = false;

    [SerializeField] private LayerMask grabLayer;
    [SerializeField] private float GrabRange = 1f;
    private float GrabMinDist = 0.1f;

    [SerializeField] private float GrabForce = 10f;

    [SerializeField] private Transform GrabLocation;

    private GameObject grabbedObject;
    private Rigidbody2D grabbedObjectRgb2D;
    // Start is called before the first frame update

    void Update()
    {
        //Check to see if we are too far from grabbed object
        if (grabbing&&Vector2.Distance(GrabLocation.position, grabbedObject.transform.position)>GrabRange){
            grabbing=false;
            grabbedObject = null;
            grabbedObjectRgb2D = null;
        }
        DrawDebugLine();
        //Pressing the grab button
        if (Input.GetKeyUp(KeyCode.LeftControl)){

            Debug.Log("Grab button pressed");

            Physics2D.queriesStartInColliders = false;

            if(!grabbing){
                RaycastHit2D toGrab = Physics2D.Raycast(transform.position, Vector2.right*transform.localScale.x, GrabRange, grabLayer);
                if (toGrab.transform !=null){
                    Debug.Log("Grabbable object hit");
                    grabbing = true;
                    grabbedObject = toGrab.transform.gameObject;
                    grabbedObjectRgb2D = grabbedObject.GetComponent<Rigidbody2D>();
                }
            }
            //Logic to release grabbed object on second key press
            else{
                grabbing=false;
                grabbedObject = null;
                grabbedObjectRgb2D = null;
            }
        }

        if (grabbing){
            PullObject();
        }
    }

    void PullObject(){

        //Only pull the object if it isnt too close
        if (Mathf.Abs(GrabLocation.position.x - grabbedObject.transform.position.x)>GrabMinDist){
            grabbedObjectRgb2D.AddForce((GrabLocation.position - grabbedObject.transform.position)*(GrabForce));
        }
        //Otherwise, just set its x position (not setting y to avoid pushing it into the floor)
        else{
            grabbedObject.transform.position = new Vector2(GrabLocation.transform.position.x, grabbedObject.transform.position.y);
        }

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject == grabbedObject){
            Physics2D.IgnoreCollision(grabbedObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
        }
    }

    void OnDrawGizmosSelected()
    {
        if (grabbedObject!=null){
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(GrabLocation.position, grabbedObject.transform.position);
        }
    }

    void DrawDebugLine(){
        if (grabbing){

        }
        else {
            Debug.DrawLine(transform.position, new Vector2(transform.position.x + (GrabRange*Mathf.Sign(transform.localScale.x)), transform.position.y), Color.red);
        }
    }

    
}
