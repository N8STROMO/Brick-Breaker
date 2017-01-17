using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    Rigidbody2D rb2d;
    public float speed;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        bool movementRight = Input.GetKey(KeyCode.RightArrow);
        bool movementLeft = Input.GetKey(KeyCode.LeftArrow);

        if (movementRight)
        {
            rb2d.velocity = new Vector2 (speed, 0);
        }

        else if (movementLeft)
        {
            rb2d.velocity = new Vector2 (-speed, 0);
        }
         
        else
        {
            rb2d.velocity = Vector2.zero;
        }
        
    }
   
}
