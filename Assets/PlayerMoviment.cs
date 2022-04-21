using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    public float jumpForce = 15.0f;
    private bool doubleJump, isJumping, isGround;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private SpriteRenderer[] spLegs;
    private Animator anim;

    private Collision2D player;

    public int movementSpeed = 300;

    private int jumpCounter = 2;
    int punchHash = Animator.StringToHash("isPunch");

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        spLegs = this.GetComponentsInChildren<SpriteRenderer>(); 
        anim = gameObject.GetComponent<Animator>();
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

    private void HorizontalMoviment(){
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        anim.SetFloat("speed", speed);
        
        if(Mathf.Abs(x) <= 10e-6){
            anim.SetFloat("speed", 0);
        }else if(x > 10e-6){
            sp.flipX = false;
            foreach (var spLeg in spLegs){
                spLeg.flipX = false;
            }
        }else if(x < -10e-6){
            sp.flipX = true;
            foreach (var spLeg in spLegs){
                spLeg.flipX = true;
            }
        }

        rb.velocity = new Vector2(x*movementSpeed, rb.velocity.y);
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
        Debug.Log("PEGOU");
        transform.position = new Vector3(-4.02f, 1.55f, 0f);
    }
}
