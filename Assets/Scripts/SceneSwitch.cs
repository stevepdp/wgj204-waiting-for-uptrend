using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour { 

    void LateUpdate()
    {
        Switch();
    }

    void Switch() 
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // switch scenes
        if (Input.GetKeyDown("space"))
        {
            switch (sceneName)
            {
                case "00_Start":
                    SceneManager.LoadScene("10_Game");
                    break;             

                case "95_Zero":
                    SceneManager.LoadScene("00_Start");
                    break;

                case "96_Loss":
                    SceneManager.LoadScene("00_Start");
                    break;

                case "97_Profit":
                    SceneManager.LoadScene("00_Start");
                    break;

                case "98_Doubled":
                    SceneManager.LoadScene("00_Start");
                    break;

                case "99_Moon":
                    SceneManager.LoadScene("00_Start");
                    break;
            }
        }
    }
}
