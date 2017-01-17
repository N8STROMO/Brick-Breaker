using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float initialXSpeed;
    public float initialYSpeed;

    void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(initialXSpeed, -initialYSpeed);
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bounds"))
        {
            ResetBall();
        }
    }

    void ResetBall()
    {
        transform.position = new Vector3(0, -1, 0);
        rb2d.velocity = new Vector2(initialXSpeed, initialYSpeed);
    }
}
