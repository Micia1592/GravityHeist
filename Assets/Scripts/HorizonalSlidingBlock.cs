using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonalSlidingBlock : MonoBehaviour
{

    [SerializeField] private float maxDisplacement;

    private Vector2 startPosition;

    [SerializeField] private float moveSpeed = 1f;
    private float localSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        startPosition = this.transform.position;
        localSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //If were outside our set bounds, reverse speed
        if (Mathf.Abs(startPosition.x - transform.position.x)>maxDisplacement){

            localSpeed = -localSpeed;
        }

        transform.Translate(localSpeed*Time.deltaTime,0,0);
    }
}
