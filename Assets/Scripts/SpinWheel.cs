using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{
  
    void Update()
    {
        transform.Rotate(Vector3.forward * -1);
    }
}
