using System;
using UnityEngine;


public class GameStateController : MonoBehaviour
{
    //Event for global gravity changing
    public static event Action<bool> OnGravityChange;
    public bool gravityInverted = false;

    //public  debrisLayer;

    private void Start() {
        //Ignore collision between all layers and debris, apart from groundable objects
        Physics2D.IgnoreLayerCollision(0, 13, true);
        Physics2D.IgnoreLayerCollision(8, 13, true);
        Physics2D.IgnoreLayerCollision(9, 13, true);
        Physics2D.IgnoreLayerCollision(13, 13, true);
        
    }

    public bool GetGravityInverted(){
        return gravityInverted;
    }

    public void SetGravityInverted(bool value){
        gravityInverted = value;
        OnGravityChange?.Invoke(gravityInverted);
    }

    public void SetGravityInverted(){
        this.SetGravityInverted(!gravityInverted);
    }
}
