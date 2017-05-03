using UnityEngine;

public class Paddle : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed;

    /// <summary>
    /// Call on first frame
    /// </summary>
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Deals with the movement of the paddle
    /// </summary>
    void FixedUpdate()
    {
        //Created two boolean variables to determine if left or right arrow is pressed
        bool movementRight = Input.GetKey(KeyCode.RightArrow);
        bool movementLeft = Input.GetKey(KeyCode.LeftArrow);

        //Controls the movement based on which arrow key is selected
        //If movement right set new velocity to a positive speed vector, no movement on y
        if (movementRight)
        {
            rb2d.velocity = new Vector2 (speed, 0);
        }

        //If movement left set new velocity to a negative speed vector, no movement on y
        else if (movementLeft)
        {
            rb2d.velocity = new Vector2 (-speed, 0);
        }
        
        //Default condition needed in nase neither arrow key is being pressed
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }
}
