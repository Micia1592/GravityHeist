using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEffected : MonoBehaviour
{
    private bool gravityInverted = false;

    public void reverseGravity(){
        GetComponent<Rigidbody2D>().gravityScale *= -1;
        transform.Rotate(Vector3.forward * 180);
        gravityInverted= !gravityInverted;
    }
}
