using System.Collections;
using System.Collections.Generic;
using Car;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadPlayers(ref List<PlayerDTO> playerDTOs)
    {
        string url = "http://localhost:8080/load-players";

        UnityWebRequest request = UnityWebRequest.Get(url);

        StartCoroutine(GetPlayers(request, playerDTOs));
    }
    
    private IEnumerator GetPlayers(UnityWebRequest request,  List<PlayerDTO> playerDTOs)
    {

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string responsePayload = request.downloadHandler.text;
            Debug.Log("Response: " + request.downloadHandler.text);

            // Process the response payload
            playerDTOs = JsonConvert.DeserializeObject<List<PlayerDTO>>(responsePayload);
        }
        
        yield return request.SendWebRequest();

    }
    
    public void LoadSelected(ref List<PlayerDTO> players, ref Player player)
    {
        string url = "http://localhost:8080/load-selected";

        UnityWebRequest request = UnityWebRequest.Get(url);

        StartCoroutine(GetSelected(request, player));
        
        players.Add(player.playerData);
    }
    
    private IEnumerator GetSelected(UnityWebRequest request, Player player)
    {

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string responsePayload = request.downloadHandler.text;
            

            // Process the response payload
            PlayerDTO tempDTO = JsonConvert.DeserializeObject<PlayerDTO>(responsePayload);
            
            Debug.Log("Response: " + request.downloadHandler.text);
            
            player.LoadPlayerData(tempDTO);
        }
        yield return request.SendWebRequest();

    }
}
