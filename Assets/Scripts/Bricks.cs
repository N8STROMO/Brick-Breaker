using UnityEngine;
using UnityEngine.SceneManagement;

public class Bricks : MonoBehaviour {

    public GameControl control;
    public int lives;
    
    /// <summary>
    /// Call on first frame
    /// </summary>
    private void Awake()
    {
        SwitchColor();
    }

    /// <summary>
    /// Deals with collisions, ball with bricks
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //If the ball collides with the brick game object; Brick looses one life; check to see if you have won
        if (collision.gameObject.CompareTag("Ball"))
        {
            lives--;
            SwitchColor();
            control.CheckWinCondition();
        }
    }

    /// <summary>
    /// Controls the brick color based on lives
    /// </summary>
    private void SwitchColor()
    {
        switch (lives)
        {
            case 5:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 4:
                gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                break;
            case 3:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case 2:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 1:
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
            case 0:
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}