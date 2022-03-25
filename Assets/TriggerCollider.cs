using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    private Collision2D player;
    private Vector3 currentScale;
    // Start is called before the first frame update
    void Start()
    {
        currentScale = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Debug.Log("atirar");
            this.gameObject.transform.position = new Vector3(player.gameObject.transform.position.x+1, player.gameObject.transform.position.y, player.gameObject.transform.position.z);
            this.gameObject.transform.localScale = currentScale;
            
        }
        if(player != null){
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x+0.035f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("bati");
        this.gameObject.transform.localScale = new Vector3(0,0,0);
        player = other;
    }
}
