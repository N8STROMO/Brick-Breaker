using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  #region Data
  public static GameManager instance;

  private Ball ball;
  private Trajectory ballTrajectory;
  private Paddle paddle;

  private Transform bricks;

  public GameObject[] brickListByLife;

  private Text Lives;
  private Text loseText;

  private List<OnLifeChangeListener> lifeChangeListeners = new List<OnLifeChangeListener>();

  private string currentScene;

  [SerializeField]
  private int _lives;
  #endregion

  public interface OnLifeChangeListener
  {
    void OnLifeChanged(int numLives);
  }

  // Is this part of data?
  public int lives
  {
    get
    {
      return _lives;
    }
    set
    {
      _lives = value;
      int numListeners = lifeChangeListeners.Count;
      for(int i = 0; i < numListeners; i++)
      {
        lifeChangeListeners[i].OnLifeChanged(_lives);
      }
    }
  }

  private void Awake()
  {
    // Using a singleton pattern for convienence.
    instance = this;

    currentScene = SceneManager.GetActiveScene().name;
  }

  // Resets paddle and ball to orginal position and speed after losing a life.
  public void ResetAfterLoseLife()
  {
    // Return the ball to it's initial position. 
    ball.gameObject.transform.position = new Vector3(0, -3.3f, 0);
    // Return the paddle to it's initial position.
    paddle.gameObject.transform.position = new Vector3(0, -3.75f, 0);
    // Set the ball speed to 0.
    ball.rb2d.velocity = new Vector2(0, 0);
    // Activate the trajectory sprite.
    ballTrajectory.gameObject.SetActive(true);
  }

  // Checks to see if you have won the game.
  public void CheckWinCondition()
  {
    int numBricks = bricks.childCount;
    bool didWin = true;

    // Itterate through the bricks.
    for (int i = 0; i < numBricks; i++)
    {
      // If there are still bricks remaining, you have not won.
      if (bricks.GetChild(i).gameObject.activeSelf)
      {
        didWin = false;
        break;
      }
    }

    // If all the bricks have been destroyed call method YouWin().
    // Where is didWin changed to true?
    if (didWin)
    {
      YouWin();
    }
  }

  // Deals with loosing lives when the ball leaves the lower bounds.
  public void LoseLife()
  {
    lives--;
    Lives.text = lives + "";

    if (lives <= 0)
    {
      Youlose();
    }
  }

  public void AddLives()
  {
    lives++;
    Lives.text = lives + "";
  }

  // Deals with winning the game and moving to the next level.
  void YouWin()
  {
    switch (currentScene)
    {
      case "Level One":
        SceneManager.LoadScene("Level Two");
        break;
      case "Level Two":
        SceneManager.LoadScene("Level Three");
        break;
      default:
        break;
    }
  }

  // Deals with lossing; the ball and paddle disappear and loseText is displayed
  void Youlose()
  {
    // Remove the ball.
    ball.gameObject.SetActive(false);
    // Remove the paddle.
    paddle.gameObject.SetActive(false);

    int numBricks = bricks.childCount;

    for (int i = 0; i < numBricks; i++)
    {
      //If there are still bricks remaining, you have not won.
      if (bricks.GetChild(i).gameObject.activeSelf)
      {
        bricks.gameObject.SetActive(false);
      }
    }

    loseText.gameObject.SetActive(true);
  }

  public void AddOnLifeChangeListener(OnLifeChangeListener listener)
  {
    lifeChangeListeners.Add(listener);
  }
}
