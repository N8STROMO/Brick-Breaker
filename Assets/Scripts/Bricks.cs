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
            switch(lives)
            {
                case 1:
                    lives = 10;
                    gameObject.GetComponent<Renderer>().material.color = Color.red; 
                    break;
                case 2:
                    lives = 5;
                    gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
