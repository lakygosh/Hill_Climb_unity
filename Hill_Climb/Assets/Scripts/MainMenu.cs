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
        SceneManager.LoadScene("LocalMultiPlayer");
        
    }
    
    
    public static void Leaderboard()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
        
    }

    private void Update()
    {
    }
}
