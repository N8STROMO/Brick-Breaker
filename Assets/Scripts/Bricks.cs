using UnityEngine;

/* 
 * 
 * */

public class Bricks : MonoBehaviour
{
  [SerializeField]
  private int lives;

  /// <summary>
  /// Deals with collisions, ball with bricks
  /// </summary>
  /// <param name="collision"></param>
  private void OnCollisionEnter2D(Collision2D collision)
  {
    //If the ball collides with the brick game object; Brick looses one life; check to see if you have won
    if(collision.gameObject.CompareTag("Ball"))
    {
      lives--;
      SwitchBrickArt(); // Updates the brick art, or destroys when dead
      GameManager.instance.CheckWinCondition();
    }
  }


  /// <summary>
  /// Destroys the current game object and (if still alive) loads another representing the current life.
  /// </summary>
  private void SwitchBrickArt()
  {
    if(lives > 0)
    {
      GameObject brickResource = GameManager.instance.brickListByLife[lives - 1];
      Instantiate(brickResource, transform.position, transform.rotation, transform.parent); // TODO use an object pool
    }

    Destroy(gameObject);
  }
}