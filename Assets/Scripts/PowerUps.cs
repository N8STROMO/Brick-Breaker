using UnityEngine;

/*
 * 
 * */

public class PowerUps : MonoBehaviour
{
  [Header("Game Objects")]
  Renderer renderer;
  [SerializeField]
  Ball ball;
  [SerializeField]
  Paddle paddle;
  [SerializeField]
  PowerUpArt[] artStyleList;

  //
  private PowerUpTypes currentActivePowerUp, previousPowerUp;
 
  //
  private int paddleCollisions;

  /// <summary>
  /// Considers the currentActivePowerUp to determine if any power up is active.
  /// </summary>
  private bool powerUpActive
  {
    get
    {
      return currentActivePowerUp != PowerUpTypes.NONE || previousPowerUp != PowerUpTypes.NONE;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  void Start()
  {
    renderer = GetComponent<Renderer>();
    GameManager.instance.onLifeChange += ResetPowerUps;
  }

  /// <summary>
  /// Enumerate the types of power ups
  /// </summary>
  public enum PowerUpTypes
  {
    NONE,
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

    if (!powerUpActive)
    {
      //If that random number is greater than 0 or less than 10, assign slow power up
      if (randomNumber > 0 && randomNumber < 30)
      {
        SetPowerUp(PowerUpTypes.SLOW);
      }

      //If the random number is greater than 10 and less than 20, assign the increase paddle size power up
      else if (paddle.canGrow && randomNumber > 30 && randomNumber < 60)
      {
        SetPowerUp(PowerUpTypes.INCREASE_PADDLE);
      }

      //If the random number is greater than 20 and less than 30, assign the add life power up
      else if (randomNumber > 60 && randomNumber < 90)
      {
        SetPowerUp(PowerUpTypes.ADD_LIFE);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="newPowerUpType"></param>
  void SetPowerUp(PowerUpTypes newPowerUpType)
  {
    if(currentActivePowerUp == newPowerUpType)
    {
      return;
    }

    GetGameObjectForArtStyle(currentActivePowerUp).SetActive(false);
    previousPowerUp = currentActivePowerUp;
    currentActivePowerUp = newPowerUpType;
    GetGameObjectForArtStyle(currentActivePowerUp).SetActive(true);
  }

  /// <summary>
  /// Checks the art style list for the selected power up.
  /// This could be improved to select from many possible art styles if desirable.
  /// </summary>
  GameObject GetGameObjectForArtStyle(PowerUpTypes powerUpType)
  {
    for(int i = 0; i < artStyleList.Length; i++)
    {
      PowerUpArt art = artStyleList[i];
      if(art.powerUpType == powerUpType)
      {
        return art.gameObject;
      }
    }

    // Should never happen
    Debug.Assert(false);
    return null; 
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
    if (powerUpActive)
    {
      switch (currentActivePowerUp)
      {
        //If the power up is slow reduce velocity multiplyer by half
        case PowerUpTypes.SLOW:
          ball.SetVelocityMultiplier(.6f);
          break;
        //If the power up is increase paddle increase the length of the paddle
        case PowerUpTypes.INCREASE_PADDLE:
          paddle.currentSize++;
          break;
        //If the pwoer up is add lives add an additional life to your current lives
        case PowerUpTypes.ADD_LIFE:
          GameManager.instance.AddLives();
          break;
      }

      //If the power up is collected record the number of collisions with the paddle
      paddleCollisions++;
      SetPowerUp(PowerUpTypes.NONE);
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

      switch(previousPowerUp)
      {
        //If the power up was slow, normalize the speed and set the paddleCollisions to 0
        case PowerUpTypes.SLOW:
          ball.SetVelocityMultiplier(1);
          break;
        //If the power us was increase paddle, return the paddle to the original size
        case PowerUpTypes.INCREASE_PADDLE:
          // TODO: Cap max size.  
          // TODO: If at max size, prevent increase powerup from spawner
          // TODO: Looking for 3 potential sizes
          paddle.currentSize--;
          break;
        case PowerUpTypes.ADD_LIFE:

          break;
      }

      ResetPowerUps();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void ResetPowerUps()
  {
    SetPowerUp(PowerUpTypes.NONE);
    previousPowerUp = PowerUpTypes.NONE;
    paddleCollisions = 0;
  }
}






