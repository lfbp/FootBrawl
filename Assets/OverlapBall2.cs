using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapBall : MonoBehaviour
{
    public Transform wallCheck;
    public LayerMask wallLayer;
    public float radiusWallCheck;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckOverlap();
    }

    void CheckOverlap(){
        bool isWall = Physics2D.OverlapCircle(wallCheck.position, radiusWallCheck, wallLayer);
        if(isWall){
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
    }
}
