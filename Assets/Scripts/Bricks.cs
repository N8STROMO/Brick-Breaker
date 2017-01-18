using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {

    public int lives = 1;

    //Method to deal with collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the ball collides with the brick game object
        if (collision.gameObject.CompareTag("Ball"))
        {
            // take one away from lives
            // Switch statement on the number of lives you have where case 0: sets active to false
            //Set the game object active state to false;
            gameObject.SetActive(false);
        }
    }
}
