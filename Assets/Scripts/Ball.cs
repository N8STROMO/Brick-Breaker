using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Rigidbody2D rb2d;
    public GameControl control;
    public Transform paddle;
    public PowerUps powerUp;
    public Trajectory trajectory;
    public bool gameHasStarted = false;
    public Vector2 ballMaxSpeed = new Vector2();
    public Vector2 ballInitialSpeed = new Vector2();
    public float velocityMultiplier;

    /// <summary>
    /// Call on first frame
    /// </summary>
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Deals with creating a velocity multiplier
    /// </summary>
    /// <param name="x"></param>
    public void SetVelocityMultiplier(float x) {
        velocityMultiplier = x;
        rb2d.velocity = rb2d.velocity * x;
    }

    /// <summary>
    /// Called on every frame
    /// </summary>
    void Update()
    {
        //Sets intial speed of ball if left or right arrow is pressed and game has started
        //Change gameHasStarted to true
        if ((Input.GetKey(KeyCode.UpArrow)) && !gameHasStarted)
        {
            gameHasStarted = true;
            rb2d.velocity = ballInitialSpeed;

        }

        //If the game has not started make the ball follow the paddle
        else if (!gameHasStarted)
        {
            transform.position = new Vector2(paddle.position.x, transform.position.y);
            rb2d.velocity = new Vector2(0, 0);
        }
    }

    /// <summary>
    /// Deals with unexpected glitch where ball continually moves from left to right barriers
    /// </summary>
    void FixedUpdate()
    {
        float currentXVelocity = rb2d.velocity.x;
        float maxYSpeed = ballMaxSpeed.y * velocityMultiplier;
        if (Mathf.Abs(rb2d.velocity.y) < maxYSpeed && gameHasStarted)
        {
            if (rb2d.velocity.y <= 0)
            {
                rb2d.velocity = new Vector2(currentXVelocity, -maxYSpeed);
            }
            else
            {
                rb2d.velocity = new Vector2(currentXVelocity, maxYSpeed);
            }  
        }
    }

    /// <summary>
    /// Deals with collisions or triggering of the lower bounds
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        //If ball collides with lower bounds: lose life, set gameHasStarted to false
        if(collision.gameObject.CompareTag("Lower Bounds"))
        {
            control.ResetAfterLoseLife();
            control.LoseLife();
            gameHasStarted = false;
        }
    }

    /// <summary>
    /// Deals with angular ball velocity and collisions
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            //Figure out how fare right or left the ball hit the paddle
            float offsetFromCenter = rb2d.transform.position.x - collision.transform.position.x;
            float collisionLength = collision.gameObject.GetComponent<Collider2D>().bounds.size.x;
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
