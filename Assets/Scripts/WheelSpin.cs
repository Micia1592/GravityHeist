using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{

    private PlayerControls playerCtrl;
    public bool noGrav;

    private void Awake()
    {
        playerCtrl = transform.root.GetComponent<PlayerControls>();
    }

    void Update()
    {

        if (noGrav == false)
        {
            transform.Rotate(Vector3.forward * -1);
        }


        else if (noGrav == true)
        {
            transform.Rotate(Vector3.forward * 1);
        }
    }
}
