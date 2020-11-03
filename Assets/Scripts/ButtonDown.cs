using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDown : MonoBehaviour
{
    [SerializeField] private GameObject slidingDoor;
    private Rigidbody2D rgb;

    private bool playerClose = false;

    private void Start()
    {
        rgb = slidingDoor.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerClose = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerClose = false;

        }
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerClose)
        {
            Debug.Log("Player pressed top button");

            rgb.velocity = new Vector3(200, 0, 0);


        }
    }
}
