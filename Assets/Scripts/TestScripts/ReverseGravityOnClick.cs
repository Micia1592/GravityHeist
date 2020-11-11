using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityOnClick : MonoBehaviour
{
    private bool gravityInverted = false;

    private GravityObject gravityObject;

    private void Start() {
        gravityObject = GetComponent<GravityObject>();
    }

    private void OnMouseOver() {
        if (Input.GetKeyUp(KeyCode.Mouse0)){
            gravityObject.SwitchLocalGravity();
        }
    }

    
  
}
