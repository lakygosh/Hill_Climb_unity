using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class MenusBackground : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        if (!SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        }
    }
    // Start is called before the first frame update
    /*void Start()
    {
        StartCoroutine(LoadVideoUrl());
    }
    
    IEnumerator LoadVideoUrl()
    {
        string url = "http://localhost:8080/videos/background"; // Replace with your actual API URL
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading video URL: " + www.error);
            }
            else
            {
                string videoURL = www.downloadHandler.text;
                PlayVideoFromUrl(videoURL);
            }
        }
    }
    
    void PlayVideoFromUrl(string videoUrl)
    {
        videoPlayer.url = videoUrl;
        videoPlayer.Play();
    }*/


    // Update is called once per frame
    void Update()
    {
        
    }
}
