using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        float i = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        rb.velocity = new Vector2(i*100, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
    }
}
