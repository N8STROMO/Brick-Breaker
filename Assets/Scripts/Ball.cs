using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Rigidbody2D rb2d;
    public float initialXSpeed;
    public float initialYSpeed;
    public GameControl control;
    private bool gameHasStarted = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
      
    }

    void Update()
    {
        if (((Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.LeftArrow))) && !gameHasStarted)  
        {
            gameHasStarted = true;
            rb2d.velocity = new Vector2(initialXSpeed, initialYSpeed);
           
        }
    }

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
    // Not only does paddle and ball need to be reset, but the motion needs to be reset too
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Lower Bounds"))
        {
            control.LoseLife();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            float offsetFromCenter = rb2d.transform.position.x - collision.transform.position.x;
            float collisionLength = collision.gameObject.GetComponent<Collider2D>().bounds.size.x;
            float fractionFromCenter = offsetFromCenter / (collisionLength / 2);
            Vector2 oldVelocity = rb2d.velocity;
            float newVelocity = fractionFromCenter * oldVelocity.y;
            rb2d.velocity = new Vector2(newVelocity, oldVelocity.y);
        }
    }
}
