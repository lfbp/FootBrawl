using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int scorePlayer1;
    public static int scorePlayer2;
    public int maxGols = 3;
    private TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if(scorePlayer1 >= maxGols){
            text.text = "       Player Blue ganhou!!";
            scorePlayer1 = 0;
            scorePlayer2 = 0;
        }else if(scorePlayer2 >= maxGols){
            text.text = "      Player Red ganhou!!";
            scorePlayer1 = 0;
            scorePlayer2 = 0;
        }
    }

    void FixedUpdate(){
        if(scorePlayer1 > 0 || scorePlayer2 > 0){
            text.text = scorePlayer1.ToString() + "                                         " + scorePlayer2.ToString();
        }
    }
}
