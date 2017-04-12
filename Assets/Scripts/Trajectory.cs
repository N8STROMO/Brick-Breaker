using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;
    public int rotation;
    public bool gameHasStarted = false;

    /// <summary>
    /// Deals with the changing the rotation of the trajectory
    /// </summary>
    void FixedUpdate()
    {
        bool movementRight = Input.GetKey(KeyCode.D);
        bool movementLeft = Input.GetKey(KeyCode.A);
        rotation = (int)transform.rotation.eulerAngles.z;

        //If movement right move new angle of trajectory to the right
        if (movementRight && (rotation > 316 || rotation <= 45)) //
        {
            transform.Rotate(new Vector3(0, 0, -1), Space.World);
        }

        //If movement left move new angle of trajectory to the left
        else if (movementLeft && (rotation < 44 || rotation >= 315)) //
        {
            transform.Rotate(new Vector3(0, 0, 1), Space.World);
        }

        //If the game has started, make the trajectory sprite disappear
        if (ball.gameHasStarted == true)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        transform.position = new Vector2(paddle.transform.position.x, transform.position.y);
    }
}

