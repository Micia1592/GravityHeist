using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGravitySwitch : MonoBehaviour
{
    [SerializeField] private GameObject gravityAffectedObjects;

    private bool playerClose = false; 

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 8){
            playerClose = true;

            Debug.Log("Player entered button area");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 8){
            playerClose = false;

            Debug.Log("Player left button area");
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D other) {
        if (Input.GetKeyUp(KeyCode.E) && (other.gameObject.layer == 8)){
            Debug.Log("Player pressed gravity switch button");

            foreach(Transform child in gravityAffectedObjects.transform)
            {
                child.GetComponent<GravityEffected>().reverseGravity();
            }
        }
    }
    */

    private void Update() {
        if (Input.GetKeyUp(KeyCode.E)&&playerClose){
            Debug.Log("Player pressed gravity switch button");

            foreach(Transform child in gravityAffectedObjects.transform)
            {
                child.GetComponent<GravityEffected>().reverseGravity();
            }
        }
    }

}
