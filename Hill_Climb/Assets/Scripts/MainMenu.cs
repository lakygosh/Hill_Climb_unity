using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    private Slider speedSlider; // Reference to your Slider UI element
    
    private void Awake()
    {
        speedSlider = GameObject.Find("SpeedSlider").GetComponent<Slider>();
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
        float speedLimiter = speedSlider.maxValue - speedSlider.value;
        if(speedLimiter == 0)
            Player._speedLimiter = 1;
        else
            Player._speedLimiter = speedLimiter;
    }
}
