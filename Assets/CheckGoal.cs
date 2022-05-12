using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGoal : MonoBehaviour
{

    Rigidbody2D ball;
    GameObject playerBlu;
    GameObject playerRed;
    private float maxSpeed = 7.5f;
    // Start is called before the first frame update
    void Start()
    {
        playerBlu = GameObject.Find("Player Blu");
        playerRed = GameObject.Find("Player Red");
        ball = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        float xSpeed = ball.velocity.x;
        float ySpeed = ball.velocity.y;
        
        if(ball.velocity.x > Mathf.Abs(maxSpeed)){
            if(ball.velocity.x > 0){
                xSpeed = maxSpeed;
            }else{
                xSpeed = -maxSpeed;
            }
        }
        if(ball.velocity.y > Mathf.Abs(maxSpeed)){
            if(ball.velocity.y > 0){
                ySpeed = maxSpeed;
            }else{
                ySpeed = -maxSpeed;
            }
        }
        
        ball.velocity = new Vector2(xSpeed, ySpeed);

        if(other.gameObject.name == "RightGoal" || other.gameObject.name == "LeftGoal") {
            if(other.gameObject.name == "RightGoal"){
                Score.scorePlayer1 += 1;
            }
            if(other.gameObject.name == "LeftGoal"){
                Score.scorePlayer2 += 1;
            }
            ball.freezeRotation = true;
            ball.velocity = new Vector2(0, 0);
            transform.position = new Vector3(0f, 5f, 0f);
            ball.freezeRotation = false;

            playerBlu.SendMessage("ResetPlayerPosition");
            playerRed.SendMessage("ResetPlayerPosition");

            SoundManagerScript.PlaySound("gol");
            golAnimation.setGolTrigger();
        }
    }
}
