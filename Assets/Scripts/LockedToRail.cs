using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedToRail : MonoBehaviour
{
    [SerializeField] private Transform topLimit;
    [SerializeField] private Transform bottomLimit;

    private Rigidbody2D rgb2D;

    // Start is called before the first frame update
    void Start()
    {
        rgb2D = this.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (this.transform.position.y > topLimit.position.y){

            this.transform.SetPositionAndRotation(topLimit.position, this.transform.rotation);
        }
        else if (this.transform.position.y < bottomLimit.position.y){
            this.transform.SetPositionAndRotation(bottomLimit.position, this.transform.rotation);
        }
        
    }
}
