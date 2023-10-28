using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    
    private void Awake()
    {
        
    }
    
    
    
    public void PlayKontrakcija () 
    {
        SceneManager.LoadScene("KontrakcijaSP");
    }
    
    public void PlayOpustanje () 
    {
        SceneManager.LoadScene("OpustanjeSP");
        
    }
    
    
    public static void Leaderboard()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
        
    }
    
    public void Shop()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
    }

    private void Update()
    {
    }
}
