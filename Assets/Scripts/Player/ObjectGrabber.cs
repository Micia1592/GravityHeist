using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{

    public bool grabbing = false;

    [SerializeField] private LayerMask grabLayer;
    [SerializeField] private float GrabRange = 0.5f;
    private float GrabMinDist = 0.1f;

    [SerializeField] private float GrabForce = 10f;

    [SerializeField] private Transform GrabLocation;

    private GravityObject playerGravObject;

    [SerializeField] private float grabTorqueLimit = 5f;
    [SerializeField] private float grabForceLimit = 5f;

    [SerializeField] private GameObject grabEffect;
    private GameObject currGravEffect;

    public float gravEffectScaleUpFactor = 5f;

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

        //Check current forces and stop the grab if forces are too high
        //CheckForce();
    }

    void ReleaseGrab(){
        grabbing=false;
        grabbedObject.GetComponent<FixedJoint2D>().connectedBody = null;
        grabbedObject.GetComponent<FixedJoint2D>().enabled=false;
        grabbedObject = null;
        grabbedObjectRgb2D = null;
        Destroy(currGravEffect);
    }

    void GrabObject(GameObject toGrab){
        if (toGrab.GetComponent<FixedJoint2D>() != null){
            grabbing = true;
            toGrab.GetComponent<FixedJoint2D>().enabled = true;
            toGrab.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            grabbedObject = toGrab;
            currGravEffect = Instantiate(grabEffect);
            //Add gravity effect as parent of grabbed object and move it to the grabbed objects position
            currGravEffect.transform.parent = grabbedObject.transform;
            currGravEffect.transform.position = grabbedObject.transform.position;

            //Update gravity effects size to match object
            Rect grabbedObjRect = grabbedObject.transform.GetComponent<SpriteRenderer>().sprite.rect;
            Rect gravEffectRect = currGravEffect.transform.GetComponent<SpriteRenderer>().sprite.rect;
            //Debug.Log("grabbed object rect: " + grabbedObjRect);
            Debug.Log("grabbedObjRect width: " + grabbedObjRect.width + ". gravEffectRect width: " + gravEffectRect.width);
            float sizeRatio = grabbedObjRect.width/gravEffectRect.width;
            Debug.Log("Ratio: " + sizeRatio);
            //Increasing ratio by a flat amount to make it a suitable size
            sizeRatio*=gravEffectScaleUpFactor;

            Vector2 currLocalScale = currGravEffect.transform.localScale;
            currGravEffect.transform.localScale = new Vector2(currLocalScale.x * sizeRatio, currLocalScale.y * sizeRatio);

        }
    }

    void CheckForce(){
        if (grabbing){
            
            //Get the current forces on the joint
            float currForce = grabbedObject.GetComponent<FixedJoint2D>().reactionForce.magnitude;
            float currTorque = grabbedObject.GetComponent<FixedJoint2D>().reactionTorque;
            //Release the grab if the forces are too high
            if ((grabbedObject.GetComponent<FixedJoint2D>().reactionTorque>grabTorqueLimit)){
                Debug.Log("Torque too high, releasing grab");
                ReleaseGrab();
            }
            else if ((currForce>grabForceLimit)){
                Debug.Log("Force too high, releasing grab");
                ReleaseGrab();

            }
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
