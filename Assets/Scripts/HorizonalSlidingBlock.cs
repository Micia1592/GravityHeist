using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonalSlidingBlock : MonoBehaviour
{

    [SerializeField] private float maxDisplacement;

    private Vector2 startPosition;
    private Rigidbody2D rgb2D;

    private Vector2 direction = new Vector2(1, 0);
    // Start is called before the first frame update
    private void Start()
    {
        startPosition = this.transform.position;
        rgb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
