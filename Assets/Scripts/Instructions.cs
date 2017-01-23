using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
	void Update()
    {
        bool keyPress = Input.anyKey;

        if (keyPress)
        {
            SceneManager.LoadScene("Level One");
        }
    }
}
    
