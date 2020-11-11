using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravity : MonoBehaviour
{
    [SerializeField] private GameObject lowGravityObjects;

    private bool playerClose2 = false;
    public float gravity = -1f;
    public float lowGravMass = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerClose2 = true;

            Debug.Log("Player entered button area");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerClose2 = false;

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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerClose2)
        {
            Debug.Log("Player pressed low gravity switch button");

            foreach (Transform child in lowGravityObjects.transform)
            {
                GetComponent<Rigidbody2D>().gravityScale *= gravity;
                GetComponent<Rigidbody2D>().mass *= lowGravMass;
            }
        }
    }

}
