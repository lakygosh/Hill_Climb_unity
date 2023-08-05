using System.Collections.Generic;
using Car;
using UnityEngine;
using Cinemachine;
using DefaultNamespace;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private List<PlayerDTO> playerDTOs = new List<PlayerDTO>();
    [SerializeField]
    private Player selectedPlayer;
    [SerializeField] private TextMeshProUGUI welcomeMessage;


    private void Awake()
    {
        PlayersLoader.LoadAllPlayers(ref playerDTOs);
        PlayersLoader.LoadSelected(ref playerDTOs, ref selectedPlayer);
        
        welcomeMessage.text = "Welcome " + selectedPlayer.playerData.Name +"!";
    }
}