using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punchCollisionDetection : MonoBehaviour
{
    public Collider2D arm;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Startou");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Colidiu");
    }
}
