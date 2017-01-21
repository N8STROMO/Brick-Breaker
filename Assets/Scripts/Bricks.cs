using UnityEngine;
using UnityEngine.SceneManagement;

public class Bricks : MonoBehaviour {

    public GameControl control;
    public int lives;
    

    private void Awake()
    {
        SwitchColor();
    }

    //Method to deal with collisions, ball with bricks
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //If the ball collides with the brick game object
        if (collision.gameObject.CompareTag("Ball"))
        {
            lives--;
            SwitchColor();
            control.CheckWinCondition();
        }
    }

    //Method to control the brick color based on lives
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