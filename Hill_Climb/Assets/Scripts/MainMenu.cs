using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    
private void Awake()
    {
        if (!SceneManager.GetSceneByName("Background").isLoaded)
        {
            SceneManager.LoadScene("Background", LoadSceneMode.Additive);
        }
    }
    
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
    
    public static void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MainMenu");
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }
    
    public void Debug()
    {
        UnityEngine.Debug.Log("Debug");
    }
}
