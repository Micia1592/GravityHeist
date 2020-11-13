using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGravitySwitch : MonoBehaviour
{
    // Start is called before the first frame update
    private bool playerClose = false;
    public AudioSource source;

    [SerializeField] private GameStateController gameStateController;

    [SerializeField] private SpriteRenderer gravEffectSprite;

    private Material gravMaterial;

    private float currBlueLevel;

    [SerializeField] private float minBlue;
    [SerializeField] private float maxBlue;

    private bool colorDescending = true;

    [SerializeField] private float colorChangeIncrement;

    private Color gravEffectStartColor;

    private void Start() {
        gravMaterial = gravEffectSprite.transform.GetComponent<SpriteRenderer>().material;
        gravEffectStartColor = gravMaterial.color;
        currBlueLevel = gravEffectStartColor.b;

        //Debug.Log("Current blue level:" + currBlueLevel);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 8){
            playerClose = true;

            //Debug.Log("Player entered button area");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 8){
            playerClose = false;

            ResetColor();

            //Debug.Log("Player left button area");
        }
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.E)&&playerClose){
            Debug.Log("Player pressed gravity switch button");
            source.Play();
            gameStateController.SetGravityInverted();

        }

        //Blink effect
        if (playerClose){
            UpdateGravColor();
            Debug.Log("Current blue level:" + gravMaterial.color.b);
        }
    }

    private void UpdateGravColor(){

        //Change color if descending
        if (colorDescending){
            Color tempColor = gravMaterial.color;
            tempColor.b = gravMaterial.color.b-colorChangeIncrement;
            gravMaterial.color = tempColor;

        }
        //If ascending
        else {
            Color tempColor = gravMaterial.color;
            tempColor.b = gravMaterial.color.b+colorChangeIncrement;
            gravMaterial.color = tempColor;
        }

        //Check if we are below limit
        if (gravMaterial.color.b < minBlue && colorDescending){
            colorDescending = false;
        }
        //Check if we are above
        else if (gravMaterial.color.b >= maxBlue && !colorDescending){
            colorDescending = true;
        }

    }

    private void ResetColor(){
        gravMaterial.color = gravEffectStartColor;
    }
}
