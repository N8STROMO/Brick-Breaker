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
    private int lives = 3;
    //This assigns sceneName to null. I want the name of the current scene
    private string currentScene;


    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    //Method to reset paddle and ball to orginal position and speed
    public void ResetAfterLoseLife()
    {
        ball.gameObject.transform.position = new Vector3(0, (float)-3.5, 0);
        ball.rb2d.velocity = new Vector2(ball.initialXSpeed, ball.initialYSpeed);
        paddle.gameObject.transform.position = new Vector3(0, (float)-3.75, 0);

    }

    public void CheckWinCondition()
    {
        int numBricks = bricks.childCount;
        bool didWin = true;

        
        for(int i = 0; i < numBricks; i++)
        {
           if (bricks.GetChild(i).gameObject.activeSelf)
            {
                didWin = false;
                break;
            }
        }

        if (didWin)
        {
            YouWin();
        }
    }

    public void LoseLife()
    {
        lives--;
        Lives.text = lives + "";

        if(lives <= 0)
        {
            Youlose();
        }
    }

    void Youlose()
    {
        ball.rb2d.velocity = new Vector2(0, 0);
        paddle.rb2d.velocity = new Vector2(0, 0);
        loseText.gameObject.SetActive(true);
    }

    void YouWin()
    { 
        switch(currentScene)
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
}
