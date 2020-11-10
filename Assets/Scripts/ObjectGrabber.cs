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

    private GravityObject playerGravObject;

    private GameObject grabbedObject;
    private Rigidbody2D grabbedObjectRgb2D;
    // Start is called before the first frame update
    void Start(){
        
        playerGravObject = GetComponent<GravityObject>();

    }

    void Update()
    {
        
        //Draw line showing the grab range
        //DrawDebugLine();
        //Pressing the grab button
        if (Input.GetKeyUp(KeyCode.C)){

            Debug.Log("Grab button pressed");

            Physics2D.queriesStartInColliders = false;

            if(!grabbing){
                RaycastHit2D toGrab;
                //Decide on grab direction based on gravity state
                if (playerGravObject.GetGravityState()){
                    toGrab = Physics2D.Raycast(transform.position, Vector2.left*transform.localScale.x, GrabRange, grabLayer);
                }
                else{
                    toGrab = Physics2D.Raycast(transform.position, Vector2.right*transform.localScale.x, GrabRange, grabLayer);
                }
                
                if (toGrab.transform !=null){
                    Debug.Log("Grabbable object hit");
                    GrabObject(toGrab.transform.gameObject);
                }
            }
            //Logic to release grabbed object on second key press
            else{
                ReleaseGrab();
            }
        }
    }

    void ReleaseGrab(){
        grabbing=false;
        grabbedObject.GetComponent<FixedJoint2D>().connectedBody = null;
        grabbedObject.GetComponent<FixedJoint2D>().enabled=false;
        grabbedObject = null;
        grabbedObjectRgb2D = null;
    }

    void GrabObject(GameObject toGrab){
        if (toGrab.GetComponent<FixedJoint2D>() != null){
            grabbing = true;
            toGrab.GetComponent<FixedJoint2D>().enabled = true;
            toGrab.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            grabbedObject = toGrab;
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
            if (playerGravObject.GetGravityState()){
                Debug.DrawLine(transform.position, new Vector2(transform.position.x - (GrabRange*Mathf.Sign(transform.localScale.x)), transform.position.y), Color.red);
            }
            else{
                Debug.DrawLine(transform.position, new Vector2(transform.position.x + (GrabRange*Mathf.Sign(transform.localScale.x)), transform.position.y), Color.red);
            }
        }
    }

    
}
