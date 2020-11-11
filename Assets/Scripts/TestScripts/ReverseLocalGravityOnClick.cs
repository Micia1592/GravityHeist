using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseLocalGravityOnClick : MonoBehaviour
{
    private GravityObject thisGravObject;
    void Start()
    {
        thisGravObject = GetComponent<GravityObject>();
    }

    private void OnMouseOver() {
        if (Input.GetKeyUp(KeyCode.Mouse0)){
            thisGravObject.SwitchLocalGravity();
        }
    }
}
