using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGoal : MonoBehaviour
{

    Rigidbody2D ball;
    GameObject playerBlu;

    // Start is called before the first frame update
    void Start()
    {
        playerBlu = GameObject.Find("Player Blu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "RightGoal" || other.gameObject.name == "LeftGoal") {
            ball = GetComponent<Rigidbody2D>();

            ball.freezeRotation = true;
            ball.velocity = new Vector2(0, 0);
            transform.position = new Vector3(-1.4089f, 3.7411f, 0f);
            ball.freezeRotation = false;

            playerBlu.SendMessage("ResetPlayerPosition");

            Debug.Log("GOOOOOOOOOOOOOOOOOOOOOOL");
        }
    }
}
