using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Unused in final game, kept in for our test levels
//Source:https://www.youtube.com/watch?v=3r6o8CfRlA4&t=165s

public class WaterParticleRemover : MonoBehaviour
{
    LayerMask waterLayer;

    private int repeatLimit = 30;
    private int checkTimer;

    void Start(){
        waterLayer = LayerMask.GetMask("LiquidEffectLayer");
        checkTimer = repeatLimit;
    }


    // Update is called once per frame
    void Update()
    {   
        //Code to destroy the water particle if none are nearby
        //Only checks every 30 ticks to avoid too many circlecasts
        if (checkTimer<=0){
            if (Physics2D.OverlapCircleAll(transform.position, 0.5f, waterLayer).Length<2){
                //Debug.Log("No nearby water particles found. Self destructing");
                Destroy(gameObject);
            }
            checkTimer = repeatLimit;
        }
        else{
            checkTimer-=1;
        }
    }
}
