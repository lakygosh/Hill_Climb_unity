using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlaySinglePlayer () 
    {
        SceneManager.LoadScene("SinglePlayer");
    }
    public void PlayMultiPlayer () 
    {
        SceneManager.LoadScene("MultiplayerRoom");
        
    }

    public void Quit()
    { 
        Application.Quit();
    }
}
