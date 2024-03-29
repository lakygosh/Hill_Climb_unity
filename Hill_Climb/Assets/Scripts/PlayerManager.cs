using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Car;
using UnityEngine;
using Cinemachine;
using DefaultNamespace;
using dto;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private static List<PlayerDTO> _playerDTOs;
    [SerializeField] private Player selectedPlayer;
    [SerializeField] private TextMeshProUGUI welcomeMessage;
    
    
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;   
            StartCoroutine(LoadPlayers());
            StartCoroutine(LoadSelected());
        }
        
    }
    
    public IEnumerator LoadPlayers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/load-players"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading players: " + www.error);
            }
            else
            {
                string responsePayload = www.downloadHandler.text;
                
                Debug.Log("Response: " + www.downloadHandler.text);
                
                _playerDTOs = JsonConvert.DeserializeObject<List<PlayerDTO>>(responsePayload);

            }
        }
    }
    
    public IEnumerator LoadSelected()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/load-selected"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading selected player: " + www.error);
            }
            else
            {
                string responsePayload = www.downloadHandler.text;

                PlayerDTO tempData = JsonConvert.DeserializeObject<PlayerDTO>(responsePayload);
                instance.selectedPlayer.LoadPlayerData(tempData);
                Debug.Log("Parsed Player Name: " + selectedPlayer.playerData.Name);
                welcomeMessage.text = "Welcome " + selectedPlayer.playerData.Name +"!";
                
            }
        }
    }

    public static IEnumerator SaveCoins(Player player)
    {
        string jsonData = JsonConvert.SerializeObject(player.playerData);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest www = new UnityWebRequest("http://localhost:8080/save-coins", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(data);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error saving coins: " + www.error);
            }
            else
            {
                string responsePayload = www.downloadHandler.text;
            }
        }
    }
    
    public static IEnumerator SaveScore(Player player)
    {
        string jsonData = JsonConvert.SerializeObject(player.playerData);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        Debug.Log(player.playerData.BestScore);
        
        using (UnityWebRequest www = new UnityWebRequest("http://localhost:8080/save-score", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(data);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error saving new best score: " + www.error);
            }
            else
            {
                string responsePayload = www.downloadHandler.text;
            }
        }
    }
    
    public static IEnumerator SaveUnlockedCars(Player player)
    {
        string jsonData = JsonConvert.SerializeObject(player.playerData);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        
        using (UnityWebRequest www = new UnityWebRequest("http://localhost:8080/unlock-car", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(data);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error saving new car: " + www.error);
            }
            else
            {
                string responsePayload = www.downloadHandler.text;
            }
        }
    }
    
    public static IEnumerator SaveSelectedCar(Player player)
    {
        string jsonData = JsonConvert.SerializeObject(player.playerData);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        
        using (UnityWebRequest www = new UnityWebRequest("http://localhost:8080/selected-car", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(data);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error saving selected car: " + www.error);
            }
            else
            {
                string responsePayload = www.downloadHandler.text;
            }
        }
    }
    
    public static List<PlayerDTO> GetPlayerDTOs()
    {
        return _playerDTOs;
    }
    
    
    public static Player GetSelectedPlayer()
    {
        return instance.selectedPlayer;
    }
    
}