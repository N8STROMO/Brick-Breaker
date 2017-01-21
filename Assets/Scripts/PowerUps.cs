using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public Paddle paddle;
    public Ball ball;
    public GameControl control;
    public Bricks bricks;

    //private void AssignPowerUp()
    //{
    //    //If the ball collides with a brick; Is this the right way to detect a collision with the ball and bricks?
    //    if (ball.rb2d.gameObject.GetComponent<Collider2D>().CompareTag("Brick"))
    //    {
    //        //Generate a random number between 0 and 100
    //        float randomNumber = Random.Range(0, 100);

    //        //If that random number is greater than 0 or less than 25, assign slow power up
    //        if (randomNumber > 0 && randomNumber < 25)
    //        {
    //            //change the color of the ball to blue
    //            ball.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    //            //if the ball his the paddle; Again, is this the right way to detect a collision with the ball and paddle?
    //            if (ball.rb2d.gameObject.GetComponent<Collider2D>().CompareTag("Paddle"))
    //            {
    //                //Record the old velocity
    //                Vector2 oldVelocity = ball.rb2d.velocity;
    //                //Set a speed by which to slow the ball
    //                Vector2 slowSpeed = new Vector2(2, 2);
    //                //Change the speed of the ball
    //                Vector2 newVelocity = oldVelocity - slowSpeed;
    //                ball.rb2d.velocity = newVelocity;
    //                //Change the color of the ball back to white; the power up has been collected
    //                ball.gameObject.GetComponent<Renderer>().material.color = Color.white;
    //            }
    //        }

    //    }
    //}
}
