using UnityEngine;

public class PowerUps : MonoBehaviour
{
  private Ball ball;
  private Bricks brick;
  private Paddle paddle;
  private GameManager control;
  private bool powerUpActive;
  private bool powerUpCollected;
  private int paddleCollisions;
  private PowerUpTypes currentActivePowerUp;
  private SpriteRenderer renderer;


  void Start()
  {
    renderer = ball.gameObject.GetComponent<SpriteRenderer>();
  }

  //Enumerate the types of power ups
  public enum PowerUpTypes
  {
    SLOW,
    INCREASE_PADDLE,
    ADD_LIFE
  }

  /// <summary>
  /// Deals with seelcting power up based on random number generator and intervals for each power up
  /// </summary>
  public void CurrentPowerUps()
  {
    float randomNumber = Random.Range(0, 240);

    if ((randomNumber > 0 && randomNumber < 240 && !powerUpCollected && !powerUpActive))
    {
      powerUpActive = true;
      //If that random number is greater than 0 or less than 10, assign slow power up
      if (randomNumber > 0 && randomNumber < 30)
      {
        //change the color of the ball to blue
        renderer.material.color = Color.blue;
        //Set currentActivePowerUp
        currentActivePowerUp = PowerUpTypes.SLOW;
      }

      //If the random number is greater than 10 and less than 20, assign the increase paddle size power up
      else if (randomNumber > 30 && randomNumber < 60)
      {
        //change the color of the ball to yellow
        renderer.material.color = Color.yellow;
        currentActivePowerUp = PowerUpTypes.INCREASE_PADDLE;
      }

      //If the random number is greater than 20 and less than 30, assign the add life power up
      else if (randomNumber > 60 && randomNumber < 90)
      {
        renderer.material.color = Color.red;
        currentActivePowerUp = PowerUpTypes.ADD_LIFE;
      }
    }
  }

  /// <summary>
  /// Everytime a brick is hit call CurrentPowerUps() to have a change at gaining power up
  /// Deals with collecting the power up using the paddle and ending the power up
  /// </summary>
  /// <param name="collision"></param>
  public void OnCollisionEnter2D(Collision2D collision)
  {
    //If the ball collides with a brick invoke CurrentPowerUps()
    if (collision.gameObject.CompareTag("Brick"))
    {
      CurrentPowerUps();
    }

    //Use the paddle to collect the power up and check to see if power up should still be active
    if (collision.gameObject.CompareTag("Paddle"))
    {
      CollectPowerUp();

      //If the power up is collected record the number of collisions with the paddle
      if (powerUpCollected)
      {
        paddleCollisions++;
      }

      //Check the condition for ending the powerup; 3 hits to the paddle
      EndPowerUp();
    }
  }

  /// <summary>
  /// Deals with collecting power ups
  /// </summary>
  private void CollectPowerUp()
  {
    //If the power up is active and not collected change the collection status to true
    if (powerUpActive && !powerUpCollected)
    {
      powerUpCollected = true;

      switch (currentActivePowerUp)
      {
        //If the power up is slow reduce velocity multiplyer by half
        case PowerUpTypes.SLOW:
          ball.SetVelocityMultiplier(.6f);
          break;
        //If the power up is increase paddle increase the length of the paddle
        case PowerUpTypes.INCREASE_PADDLE:
          paddle.transform.localScale = new Vector2(4f, .25f);
          break;
        //If the pwoer up is add lives add an additional life to your current lives
        case PowerUpTypes.ADD_LIFE:
          control.AddLives();
          break;
      }
    }
  }

  /// <summary>
  /// Deals with ending the power up
  /// </summary>
  private void EndPowerUp()
  {
    //If the ball collides with the paddle 3 times set the powerUpActive to false and the powerUpCollected to false.
    if (paddleCollisions > 4)
    {
      powerUpActive = false;
      powerUpCollected = false;
      //Set ball to white to symbolize no power up
      ball.gameObject.GetComponent<Renderer>().material.color = Color.white;
      //reset paddle collisions
      paddleCollisions = 0;
      switch (currentActivePowerUp)
      {
        //If the power up was slow, normalize the speed and set the paddleCollisions to 0
        case PowerUpTypes.SLOW:
          ball.SetVelocityMultiplier(1);
          break;
        //If the power us was increase paddle, return the paddle to the original size
        case PowerUpTypes.INCREASE_PADDLE:
          paddle.transform.localScale = new Vector2(2.5f, .25f);
          break;
        case PowerUpTypes.ADD_LIFE:

        break;
      }
    }
  }
}






