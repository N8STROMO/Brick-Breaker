using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUps : MonoBehaviour
{
    public Ball ball;

    public void SlowPowerUp()
    {
            //Generate a random number between 0 and 100
            float randomNumber = Random.Range(0, 100);

            //If that random number is greater than 0 or less than 25, assign slow power up
            if (randomNumber > 0 && randomNumber < 25)
            {
                //change the color of the ball to blue
                ball.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                //Record the old velocity
                Vector2 oldVelocity = ball.rb2d.velocity;
                //Set a speed by which to slow the ball
                Vector2 slowSpeed = new Vector2(-2, -2);
                //Change the speed of the ball
                Vector2 newVelocity = oldVelocity - slowSpeed;
                ball.rb2d.velocity = newVelocity;
                }
            }
        }
    

