using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGravitySwitch : MonoBehaviour
{
    // Start is called before the first frame update
    private bool playerClose = false; 

    [SerializeField] private GameStateController gameStateController;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 8){
            playerClose = true;

            Debug.Log("Player entered button area");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 8){
            playerClose = false;

            Debug.Log("Player left button area");
        }
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.E)&&playerClose){
            Debug.Log("Player pressed gravity switch button");

            gameStateController.SetGravityInverted();

        }
    }
}
