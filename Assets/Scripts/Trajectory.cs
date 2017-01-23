using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Ball ball;

    /// <summary>
    /// Call on first frame
    /// </summary>
    private void Start()
    {
        rb2d.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Deals with the changing the rotation of the trajectory
    /// ToDo figure out how to make the change in rotation of trajectory reflect to the velocity of the ball
    /// </summary>
    void FixedUpdate()
    {
        bool movementRight = Input.GetKey(KeyCode.D);
        bool movementLeft = Input.GetKey(KeyCode.A);

        //If movement right set set new angle of trajectory to the right
        if (movementRight)
        {
            rb2d.transform.eulerAngles = new Vector3(0, 0, -5);
        }

        //If movement left set set new angle of trajectory to the left
        else if (movementLeft)
        {
            rb2d.transform.eulerAngles = new Vector3(0, 0, 5);
        }

        //Default condition needed in nase neither arrow key is being pressed
        else
        {
            rb2d.transform.eulerAngles = new Vector3(0, 0, 0);
            ball.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        
    }
}
