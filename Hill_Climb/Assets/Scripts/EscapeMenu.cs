using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public void Continue ()
    {
        SceneManager.UnloadSceneAsync("EscapeMenu");
    }
    
    public void Options () 
    {
        
    }
    
    public void MainMenu () 
    {
        SceneManager.LoadScene("MainMenu");
        
    }

    public void Quit()
    { 
        Application.Quit();
    }

    private void Update()
    {
        Continue();
    }
}
