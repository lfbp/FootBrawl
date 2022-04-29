using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMoviment : NetworkBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    public float jumpForce = 15.0f;
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

    private int jumpCounter = 2;
    int punchHash = Animator.StringToHash("isPunch");

    public NetworkVariable<Vector2> Velocity = new NetworkVariable<Vector2>();
    public NetworkVariable<Vector3> Scale = new NetworkVariable<Vector3>();

    public override void OnNetworkSpawn() {
        if (IsOwner) {
            ResetPlayerPosition();
        }
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        spChildren = this.GetComponentsInChildren<SpriteRenderer>(); 
        anim = gameObject.GetComponent<Animator>();
        playerObject = this.gameObject;
    }


    // Update is called once per frame
    void Update() {
        if (IsOwner) {
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

        playerObject.transform.localScale = Scale.Value;
        rb.velocity = Velocity.Value;
    }

    private void HorizontalMoviment(){
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        anim.SetFloat("speed", speed);
        
        if(Mathf.Abs(x) <= zero){
            anim.SetFloat("speed", 0);
        }
        else{
            if(IsServer) {
                if(x > zero){
                    if(playerObject.transform.localScale.x < zero){
                        Flip();
                    }
                }else if(x < -zero){
                    if(playerObject.transform.localScale.x >= zero){
                        Flip();
                    }
                }
            }
            else {
                if(x > zero){
                    if(playerObject.transform.localScale.x >= zero){
                        Flip();
                    }
                }else if(x < -zero){
                    if(playerObject.transform.localScale.x < zero){
                        Flip();
                    }
                }
            }
        } 

        SubmitVelocityRequestServerRpc(new Vector2(x*movementSpeed, rb.velocity.y));
        // rb.velocity = new Vector2(x*movementSpeed, rb.velocity.y);
    }

    private void Flip(){
        Vector3 scale = playerObject.transform.localScale;
        scale.x  *= -1;
        SubmitScaleRequestServerRpc(scale);
        //playerObject.transform.localScale = scale;
    }

    void Jump(){
        SubmitJumpRequestServerRpc();
        // rb.velocity = Vector2.up * jumpForce;
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Ground"){
            jumpCounter = 2;
        }
    }

    public void ResetPlayerPosition() {
        SubmitScaleRequestServerRpc(new Vector3(2.0228f, 2.0228f, 2.0228f));
    }

    [ServerRpc]
    void SubmitVelocityRequestServerRpc(Vector2 position, ServerRpcParams rpcParams = default) {
        Velocity.Value = position;
    }

    [ServerRpc]
    void SubmitScaleRequestServerRpc(Vector3 scale, ServerRpcParams rpcParams = default) {
        Scale.Value = scale;
    }

    [ServerRpc]
    void SubmitJumpRequestServerRpc(ServerRpcParams rpcParams = default) {
        Velocity.Value = Vector2.up * jumpForce;
    }
}
