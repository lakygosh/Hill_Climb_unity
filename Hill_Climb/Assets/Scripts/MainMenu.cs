using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    
    private void Awake()
    {
        
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
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
        
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
