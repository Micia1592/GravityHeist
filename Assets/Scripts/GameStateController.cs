using System;
using UnityEngine;


public class GameStateController : MonoBehaviour
{
    //Event for global gravity changing
    public static event Action<bool> OnGravityChange;
    public bool gravityInverted = false;

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
