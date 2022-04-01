using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    public float jumpForce;
    private bool doubleJump, isJumping, isGround;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;

    private Collision2D player;

    public int movementSpeed = 300;

    //private float radiusGroundCheck = 0.1f;

    private int jumpCounter = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        HorizontalMoviment();

        //isGround = Physics2D.OverlapCircle(groundCheck.position, radiusGroundCheck, groundLayer);
        
        //if(isGround){
        //    Debug.Log("Entrou no ground");
        //    jumpCounter = 2;
        //}
        

        if (Input.GetKeyDown(KeyCode.Space)) {
            if(jumpCounter > 0){
                Jump();

                jumpCounter--;
            }
        }
        
    }

    private void HorizontalMoviment(){
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        //transform.Translate(x * movementSpeed, y * movementSpeed, 0);

        rb.velocity = new Vector2(x*movementSpeed, rb.velocity.y);
    }

    void Jump(){
        rb.velocity = Vector2.up * jumpForce;
        //rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Ground"){
            jumpCounter = 2;
        }
    }
}
