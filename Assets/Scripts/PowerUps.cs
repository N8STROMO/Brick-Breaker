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
    private PowerUpTypes currentActivePowerUp;

    public enum PowerUpTypes {
        SLOW,
        INCREASE_PADDLE,
        ADD_LIFE
    }
    
    public void ChanceSlowPowerUp()
    {
        float randomNumber = Random.Range(0, 100);

        
        if ((randomNumber > 0 && randomNumber < 30 && !powerUpCollected))
        {
            powerUpActive = true;
            //If that random number is greater than 0 or less than 10, assign slow power up
            if (randomNumber > 0 && randomNumber < 10)
            {
                //change the color of the ball to blue
                ball.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                //Set currentActivePowerUp
                currentActivePowerUp = PowerUpTypes.SLOW;
            }

            if (randomNumber > 10 && randomNumber < 20)
            {
                //change the color of the ball to yellow
                ball.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                currentActivePowerUp = PowerUpTypes.INCREASE_PADDLE;
            }
            
            if (randomNumber > 20 && randomNumber <30)
            {
                ball.gameObject.GetComponent<Renderer>().material.color = Color.red;
                currentActivePowerUp = PowerUpTypes.ADD_LIFE;
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

            switch (currentActivePowerUp) {
                case PowerUpTypes.SLOW:
                    ball.SetVelocityMultiplier(.5f);
                    break;
                case PowerUpTypes.INCREASE_PADDLE:
                    break;
                case PowerUpTypes.ADD_LIFE:
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

   
    

    

