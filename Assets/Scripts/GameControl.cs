using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{ 

    public Ball ball;
    public Paddle paddle;

    public void LoseLife()
    {
        ball.gameObject.transform.position = new Vector3(0, (float)-3.5, 0);
        ball.rb2d.velocity = new Vector2(ball.initialXSpeed, ball.initialYSpeed);
        paddle.gameObject.transform.position = new Vector3(0, (float)-3.75, 0);
    }
}
