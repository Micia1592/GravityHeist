using System;
using UnityEngine;

//This scriptcontrols each levels gravity

public class GameStateController : MonoBehaviour
{
    //Event for global gravity changing
    public static event Action<bool> OnGravityChange;
    public bool gravityInverted = false;

    //Setting for each level on whether the grav gun is equiped 
    public bool IsGravGunEquiped;

    private PlayerControls player;
    private GameObject gravgun;

    //public  debrisLayer;

    private void Awake() {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        if (player==null){
            Debug.Log("Game state controller failed to find player by tag. Please investigate");
        }
        gravgun = GameObject.FindGameObjectWithTag("GravGun");

        if (gravgun==null){
            Debug.Log("Game state controller failed to find grav gun by tag. Please investigate");
        }

        //Equip the gravgun 
        EquipGravGun(IsGravGunEquiped);



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

    public void EquipGravGun(bool equip){
        IsGravGunEquiped = equip;
        if (gravgun==null){
            Debug.Log("Gravgun is null. Please investigate");
        }
        if (IsGravGunEquiped){
            gravgun.GetComponent<Gravgun>().enabled = true;
            gravgun.GetComponent<SpriteRenderer>().enabled = true;
        }
        else{
            gravgun.GetComponent<Gravgun>().enabled = false;
            gravgun.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
