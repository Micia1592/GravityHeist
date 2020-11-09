using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{

    private PlayerControls playerCtrl;
    public bool noGrav;
    public GameStateController gameStateController;

    private void Awake()
    {
        playerCtrl = transform.root.GetComponent<PlayerControls>();
    }

    void Update()
    {

        if (gameStateController.gravityInverted == true && noGrav == true)
        {
            transform.Rotate(Vector3.forward * -1);
        }

        else if (gameStateController.gravityInverted == false && noGrav == false)
        {
            transform.Rotate(Vector3.forward * 1);
        }

        else if (gameStateController.gravityInverted == true && noGrav == false)
        {
            transform.Rotate(Vector3.forward * 1);
        }

        else if (gameStateController.gravityInverted == false && noGrav == true)
        {
            transform.Rotate(Vector3.forward * -1);
        }

        else
        {
            transform.Rotate(Vector3.forward * 1);
        }
    }
}
