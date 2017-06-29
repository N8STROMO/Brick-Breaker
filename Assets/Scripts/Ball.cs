using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{

  public Rigidbody2D rb2d;
  public Transform paddle;
  public PowerUps powerUp;
  public Trajectory trajectory;
  public Vector2 ballMaxSpeed;
  public bool gameHasStarted = false;
  public float SpeedX;
  public float SpeedY;
  public float velocityMultiplier;
  public int offset;

  /// <summary>
  /// Call on first frame
  /// </summary>
  private void Start()
  {
    StartCoroutine(LaunchBallAndStartGame());
  }

  /// <summary>
  /// Deals with unexpected glitch where ball continually moves from left to right barriers
  /// </summary>
  void FixedUpdate()
  {
    float currentXVelocity = rb2d.velocity.x;
    float maxYSpeed = ballMaxSpeed.y * velocityMultiplier;
    if(Mathf.Abs(rb2d.velocity.y) < maxYSpeed && gameHasStarted)
    {
      if(rb2d.velocity.y <= 0)
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
  /// Deals with creating a velocity multiplier
  /// </summary>
  /// <param name="x"></param>
  public void SetVelocityMultiplier(float x)
  {
    velocityMultiplier = x;
    rb2d.velocity = rb2d.velocity * x;
  }

  /// <summary>
  /// Called on every frame
  /// ToDo set the initial X velocity as a component of the trajectory.
  /// </summary>
  void Update()
  {
    SettingDegrees();

    //The X velocity is a product of tanget where the offset agle where the ...? 
    SpeedX = Mathf.Clamp(SpeedY / Mathf.Tan(Mathf.Deg2Rad * offset), -ballMaxSpeed.x, ballMaxSpeed.x);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private IEnumerator<float> LaunchBallAndStartGame()
  {
    gameHasStarted = false;

    // Calculate the ball's offset from the paddle's center position.
    BoxCollider2D paddleCollider = paddle.GetComponentInChildren<BoxCollider2D>();
    CircleCollider2D ballCollider = GetComponentInChildren<CircleCollider2D>();
    Vector3 offset = new Vector3(0,
      (paddleCollider.bounds.size.y
      + ballCollider.bounds.size.y) / 2,
      0);

    // While the user 
    while(Input.GetKey(KeyCode.UpArrow) == false)
    { // Track the paddle's position each frame, until up is pressed
      transform.position = paddle.transform.position + offset;
      yield return 0;
    }

    //Sets intial speed of ball if left or right arrow is pressed and game has started
    //Change gameHasStarted to true
    gameHasStarted = true;
    rb2d.velocity
      = trajectory.transform.rotation // Apply the current aim direction to the balls launch
        * new Vector2(SpeedX, SpeedY);

    // The ball starts with physics off, once launched the ball should start physics simulation again.
    // This allows the ball to follow the paddle before launch.
    rb2d.simulated = true;
  }

  /// <summary>
  /// Deals with collisions or triggering of the lower bounds; losing a life
  /// </summary>
  /// <param name="collision"></param>
  void OnTriggerEnter2D(Collider2D collision)
  {
    //If ball collides with lower bounds: lose life, set gameHasStarted to false
    if(collision.gameObject.CompareTag("Lower Bounds"))
    {
      GameManager.instance.ResetAfterLoseLife();
      GameManager.instance.LoseLife();

      if(isActiveAndEnabled)
      { // If game is not over yet.
        StartCoroutine(LaunchBallAndStartGame());
      }
    }
  }

  /// <summary>
  /// Deals with angular ball velocity and collisions 
  /// TODO this is where the bug with the ball movement is 
  /// Unity physics vs custom physics engine
  /// </summary>
  /// <param name="collision"></param>
  public void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.CompareTag("Paddle"))
    {
      //Figure out how fare right or left the ball hit the paddle
      float offsetFromCenter = rb2d.transform.position.x - collision.transform.position.x;
      float collisionLength = collision.collider.bounds.size.x;
      float fractionFromCenter = offsetFromCenter / (collisionLength / 2);
      //Get the fraction from -1 (left) to 1 (right) of where the ball hit the paddle
      Vector2 oldVelocity = Vector2.Max(rb2d.velocity, new Vector2(0, 1));
      //Scale x velocity to the fraction of where the ball hit the paddle by the current y velocity
      // Ensure some minimum velocity
      float newVelocity = fractionFromCenter * oldVelocity.y;
      //Set the new velocity
      rb2d.velocity = new Vector2(newVelocity, oldVelocity.y);
    } else if (rb2d.velocity.sqrMagnitude < 1)
    { // If the ball comes close to stopping - just nudge it a bit.
      rb2d.velocity += new Vector2(0, 1);
    }
  }

  public void SettingDegrees()
  {
    offset = trajectory.rotation - 90;
  }
}
