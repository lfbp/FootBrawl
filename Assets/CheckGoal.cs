using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGoal : MonoBehaviour
{

    Rigidbody2D ball;
    GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("Menu");
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
            transform.position = new Vector3(-1.36f, 0.45f, 0f);
            ball.freezeRotation = false;

            menu.SendMessage("ResetPlayersPositions");

            Debug.Log("GOOOOOOOOOOOOOOOOOOOOOOL");
        }
    }
}
