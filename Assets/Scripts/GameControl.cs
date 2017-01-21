using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    //Holding reference to ball and paddle
    public Ball ball;
    public Paddle paddle;
    public Transform bricks;
    public Text Lives;
    public Text loseText;
    private static int lives = 3;
    private string currentScene;

    //On game started, get the current scene name
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    //Method to reset paddle and ball to orginal position and speed
    public void ResetAfterLoseLife()
    {
        ball.gameObject.transform.position = new Vector3(0, (float)-3.3, 0);
        ball.rb2d.velocity = new Vector2(0, 0);
        paddle.gameObject.transform.position = new Vector3(0, (float)-3.75, 0);

    }

    //Method to deal with winning the game
    public void CheckWinCondition()
    {
        int numBricks = bricks.childCount;
        bool didWin = true;

        //Itterate through the bricks
        for(int i = 0; i < numBricks; i++)
        {
           //If there are still bricks remaining, you have not won
           if (bricks.GetChild(i).gameObject.activeSelf)
            {
                didWin = false;
                break;
            }
        }
        
        //If all the bricks have been destroyed call method YouWin()
        if (didWin)
        {
            YouWin();
        }
    }

    //Method to deal with loosing lives when the ball leaves the loves bounds
    public void LoseLife()
    {
        lives--;
        Lives.text = lives + "";

        if(lives <= 0)
        {
            Youlose();
        }
    }

    //Method to deal with winning the game and moving to the next level
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

    //Method to deal with lossing; the ball and paddle disappear and loseText is displayed
    void Youlose()
    {
        ball.gameObject.SetActive (false);
        paddle.gameObject.SetActive (false);
        loseText.gameObject.SetActive(true);
    }

    
}
