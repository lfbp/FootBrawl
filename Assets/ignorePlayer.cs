using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignorePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D wall;
    void Start()
    {
        wall = GetComponent <Collider2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        int player = 8;
        if(other.gameObject.layer == player){            
            Physics2D.IgnoreCollision(wall, other.collider);
        }
    }
}
