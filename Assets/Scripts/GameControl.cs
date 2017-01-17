using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameControl : MonoBehaviour
{ 
    //Holding reference to ball and paddle
    public Ball ball;
    public Paddle paddle;
    public Text Lives;
    private int lives = 3;


    //Method to reset paddle and ball to orginal position and speed
    public void ResetAfterLoseLife()
    {
        ball.gameObject.transform.position = new Vector3(0, (float)-3.5, 0);
        ball.rb2d.velocity = new Vector2(ball.initialXSpeed, ball.initialYSpeed);
        paddle.gameObject.transform.position = new Vector3(0, (float)-3.75, 0);
  
    }

    public void LoseLife()
    {
        lives--;
        Lives.text = lives + "";
    }
}
