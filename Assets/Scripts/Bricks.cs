using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {

    public int lives;

    //Method to deal with collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the ball collides with the brick game object
        if (collision.gameObject.CompareTag("Ball"))
        {
            switch(lives)
            {
                case 3 :
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                    lives--;
                    break;
                case 2 :
                    gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    lives--;
                    break;
                case 1 :
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                    lives--;
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
