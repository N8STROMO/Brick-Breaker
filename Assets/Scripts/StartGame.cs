using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    /// <summary>
    /// After instructions are displayed press any key to start the game
    /// </summary>
	void Update () {

        bool keyPress = Input.anyKey;

        if(keyPress)
        {
            SceneManager.LoadScene("Level One");
        }


    }
}
