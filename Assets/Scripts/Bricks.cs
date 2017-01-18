using UnityEngine;
using UnityEngine.SceneManagement;

public class Bricks : MonoBehaviour {

    public int lives;
    public GameControl control;

    private void Awake()
    {
        SwitchColor();
    }
    //Method to deal with collisions, ball with bricks
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the ball collides with the brick game object
        if (collision.gameObject.CompareTag("Ball"))
        {
            lives--;
            SwitchColor();
            control.CheckWinCondition();
        }
    }
    private void SwitchColor()
    {
        switch (lives)
        {
            case 4:
                gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                break;
            case 3:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
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