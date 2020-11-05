using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonalSlidingBlock : MonoBehaviour
{

    [SerializeField] private float maxDisplacement;

    private Vector2 startPosition;

    [SerializeField] private float moveSpeed = 1f;
    private float localSpeed;

    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    private void Start()
    {
        startPosition = this.transform.position;
        localSpeed = moveSpeed;
        rb2D = this.GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(localSpeed, 0);

    }

    // Update is called once per frame
    void Update()
    {
        //If were outside our set bounds, reverse direction
        if (Mathf.Abs(startPosition.x - transform.position.x)>maxDisplacement){

            if (startPosition.x < transform.position.x){
                localSpeed = -moveSpeed;
            }
            else {
                localSpeed = moveSpeed;
            }
            rb2D.velocity = new Vector2(localSpeed, 0);

        }
    }
}
