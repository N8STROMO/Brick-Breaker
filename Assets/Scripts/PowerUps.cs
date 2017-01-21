using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUps : MonoBehaviour
{
    public Ball ball;
    public Bricks brick;
    public Paddle paddle;
    public bool powerUpActive;
    private bool powerUpCollected;
    public int paddleCollisions;
    private PowerUpTypes currentActivePowerup;

    public enum PowerUpTypes {
        SLOW,
        INCREASE_PADDLE
    }
    
    public void ChanceSlowPowerUp()
    {
        float randomNumber = Random.Range(0, 100);

        //If that random number is greater than 0 or less than 25, assign slow power up
        if ((randomNumber > 0 && randomNumber < 30 && !powerUpCollected))
        {
            powerUpActive = true;
            if (randomNumber > 0 && randomNumber < 10) {
                //change the color of the ball to blue
                ball.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                currentActivePowerup = PowerUpTypes.SLOW;
            }
            
            
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            ChanceSlowPowerUp();
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            CollectPowerUp();

            if (powerUpCollected)
            {
                paddleCollisions++;
            }
           
            EndPowerUp();
            
        }

    }

    private void CollectPowerUp()
    {
        if (powerUpActive && !powerUpCollected) {

            switch (currentActivePowerup) {
                case PowerUpTypes.SLOW:
                    ball.SetVelocityMultiplier(.5f);
                    break;
                case PowerUpTypes.INCREASE_PADDLE:
                    break;
            }
            powerUpCollected = true;
            


        }
        
    }

    private void EndPowerUp()
    {
        if (paddleCollisions > 3)
        {
            powerUpActive = false;
            powerUpCollected = false;
            ball.SetVelocityMultiplier(1);
            ball.gameObject.GetComponent<Renderer>().material.color = Color.white;
            paddleCollisions = 0;
            
        }
    }

}

   
    

    

