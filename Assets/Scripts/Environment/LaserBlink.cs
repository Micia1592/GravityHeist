using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlink : MonoBehaviour
{
    
    public GameObject laserWall;
    private BoxCollider2D laserCollider;
    private SpriteRenderer laserRenderer;

    public float fullCycleInterval;
    private float halfCycle;

    //Storage for when in each cycle the warning indicator appears letting the player know the laz0rs about to activate
    private float warningInterval;
    private bool warningActive;

    private bool deathWallActive;

    private float xScaleIncreaseIncrement;

    private float previousIncreaseTime;
    private int currentAlpha;

    private Vector2 initialScale;

    private float timeCounter = 0f;

    public AudioSource source;
  

    private void Start()
    {
        //Laser is only on for half of its full cycle. Convenient to store that half Cycle here
        halfCycle = fullCycleInterval/2;
        //Warning interval is for when in the safe half of the cycle, a warning indicator acivates to show that the laser is going to turn on
        warningInterval = halfCycle*0.6f;

        initialScale = laserWall.transform.localScale;

        //Rough calculations for how much the x scaling should increase every 0.1 seconds
        xScaleIncreaseIncrement =  initialScale.x / ((halfCycle-warningInterval)/0.01f);

        //Debug.Log("XScale increment: " + xScaleIncreaseIncrement);

        laserCollider = laserWall.GetComponent<BoxCollider2D>();
        laserRenderer = laserWall.GetComponent<SpriteRenderer>();

        laserCollider.enabled = false;
        currentAlpha = 0;
        setLaserAlpha(currentAlpha);

    }

    private void Update() {
        

        timeCounter += Time.deltaTime;

        //For when cycle is completed and we need to restart the cycle
        if(timeCounter>fullCycleInterval){
            //Debug.Log("Deactivating DeathWall, cycle complete");
            laserCollider.enabled = false;
            laserRenderer.enabled = false;
            deathWallActive=false;
            //currentAlpha = 1;
            //setLaserAlpha(currentAlpha);
            timeCounter=0F;
        }
        //For when we are need to turn the lazer on
        else if (timeCounter>halfCycle&&!deathWallActive){
            //Debug.Log("Reactivating DeathWall");
            deathWallActive = true;
            warningActive = false;
            setNewXScale(4.2454f);
            setLaserAlpha(1);
            laserCollider.enabled=true;

        }
        //For when we need to activate the warning
        else if(timeCounter<halfCycle&&timeCounter>warningInterval&&!warningActive){
            //Debug.Log("Warning activated. DeathWall activating soon");
            warningActive = true;
            laserRenderer.enabled = true;
            setNewXScale(0.5f);
            setLaserAlpha(255);
            previousIncreaseTime = 0f;
        }

        //For when the warning is active, and its been more than 0.1 seconds since the last increase, increase the alpha of the laser
        if(warningActive&&(timeCounter-previousIncreaseTime)>0.01f){
            increaseScaleIncrement();
            previousIncreaseTime = timeCounter;
        }


        
    }

    void setLaserAlpha(int newAlpha){
  
        Color tempColor = laserRenderer.material.color;
        tempColor.a = newAlpha;
        laserRenderer.material.color = tempColor;
        
    }

    void setNewXScale(float newX){
        laserWall.transform.localScale = new Vector2(newX, laserWall.transform.localScale.y);
    }

    void increaseScaleIncrement(){
        float currXScale = laserWall.transform.localScale.x;
        currXScale+=xScaleIncreaseIncrement;
        setNewXScale(currXScale);

    }
}
