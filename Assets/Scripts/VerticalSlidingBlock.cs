using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSlidingBlock : MonoBehaviour
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
        rb2D.velocity = new Vector2(0, localSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        //If were outside our set bounds, reverse direction
        if (Mathf.Abs(startPosition.y - transform.position.y)>maxDisplacement){

            if (startPosition.y < transform.position.y){
                localSpeed = -moveSpeed;
            }
            else {
                localSpeed = moveSpeed;
            }
            rb2D.velocity = new Vector2(0, localSpeed);

        }
    }
}
