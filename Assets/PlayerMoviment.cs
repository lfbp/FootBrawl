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
    private SpriteRenderer sp;
    private SpriteRenderer[] spChildren;
    private Animator anim;

    private double zero = 10e-6;

    private Collision2D player;

    private GameObject playerObject;

    public int movementSpeed = 300;

    float horizontalMove = 0f;

    private int jumpCounter = 2;
    int punchHash = Animator.StringToHash("isPunch");

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        spChildren = this.GetComponentsInChildren<SpriteRenderer>(); 
        anim = gameObject.GetComponent<Animator>();
        playerObject = this.gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        HorizontalMoviment();
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(jumpCounter > 0){
                Jump();
                jumpCounter--;
            }
        }
        
        if (Input.GetMouseButtonDown(0)){
            anim.SetTrigger(punchHash);
        }

    }

    void FixedUpdate() {
        rb.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void HorizontalMoviment(){
        horizontalMove = Input.GetAxis("Horizontal") * movementSpeed;
        anim.SetFloat("speed", speed);
        
        if(Mathf.Abs(horizontalMove) <= zero){
            anim.SetFloat("speed", 0);
        }else if(horizontalMove > zero){
            if(playerObject.transform.localScale.x < zero){
                Flip();
            }
        }else if(horizontalMove < -zero){
            if(playerObject.transform.localScale.x >= zero){
                Flip();
            }
        }
    }

    private void Flip(){
        Vector3 scale = playerObject.transform.localScale;
        scale.x  *= -1;
        playerObject.transform.localScale = scale;
    }

    void Jump(){
        rb.velocity = Vector2.up * jumpForce;
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Ground"){
            jumpCounter = 2;
        }
    }

    public void ResetPlayerPosition() {
        transform.position = new Vector3(-4.02f, 1.55f, 0f);
    }
}
