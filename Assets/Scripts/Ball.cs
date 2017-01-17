using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Rigidbody2D rb2d;
    public float initialXSpeed;
    public float initialYSpeed;
    public GameControl control;
    public bool gameHasStarted = false;
    public bool froze = false;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //Sets intial speed of ball if left or right arrow is pressed and game has started
        if ((Input.GetKey(KeyCode.UpArrow)) && !gameHasStarted)
        {
            gameHasStarted = true;
            froze = false;
            rb2d.velocity = new Vector2(initialXSpeed, initialYSpeed);

        }
        if (froze)
        {
            rb2d.velocity = new Vector2(0, 0);
        }
    }

    //Method to deal with unexpected glitch where ball continually moves from left to right barriers
    void FixedUpdate()
    {
        float currentXVelocity = rb2d.velocity.x;
        if (Mathf.Abs(rb2d.velocity.y) < initialYSpeed && gameHasStarted)
        {
            if (rb2d.velocity.y <= 0)
            {
                rb2d.velocity = new Vector2(currentXVelocity, -initialYSpeed);
            }
            else
            {
                rb2d.velocity = new Vector2(currentXVelocity, initialYSpeed);
            }
           
        }
    }

    //Method to deal with collisions
    void OnTriggerEnter2D(Collider2D collision)
    {
        //If ball collides with lower bounds
        if(collision.gameObject.CompareTag("Lower Bounds"))
        {
            control.ResetAfterLoseLife();
            control.LoseLife();
            gameHasStarted = false;
            froze = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            //Figure out how fare right or left the ball hit the paddle
            float offsetFromCenter = rb2d.transform.position.x - collision.transform.position.x;float collisionLength = collision.gameObject.GetComponent<Collider2D>().bounds.size.x;
            float fractionFromCenter = offsetFromCenter / (collisionLength / 2);
            //Get the fraction from -1 (left) to 1 (top) of where the ball hit the paddle
            Vector2 oldVelocity = rb2d.velocity;
            //Scale x velocity to the fraction of where the ball hit the paddle by the current y velocity
            float newVelocity = fractionFromCenter * oldVelocity.y;
            //Set the new velocity
            rb2d.velocity = new Vector2(newVelocity, oldVelocity.y);
        }
    }
}
