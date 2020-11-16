using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spin a wheel. This is unused in final game

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

            transform.Rotate(Vector3.forward * 1);
        
    }
}
