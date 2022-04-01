using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBallCollider : MonoBehaviour
{

    public Collider2D wall;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //CheckOverlap();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        int water = 4;
        if(other.gameObject.layer != water){
            Physics2D.IgnoreCollision(wall, other.collider);
        }
    }
}
