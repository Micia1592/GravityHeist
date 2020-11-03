using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityOnClick : MonoBehaviour
{
    private bool gravityInverted = false;

    private void OnMouseOver() {
        if (Input.GetKeyUp(KeyCode.Mouse0)){
            GetComponent<Rigidbody2D>().gravityScale *= -1;
            transform.Rotate(Vector3.forward * 180);
            gravityInverted= !gravityInverted;
        }
    }
  
}
