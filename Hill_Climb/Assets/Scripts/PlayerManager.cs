using System.Collections.Generic;
using Car;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    //private List<PlayerInput> playerInputs = new List<PlayerInput>();
    private List<Player> players = null;
    [SerializeField]
    private Player selectedPlayer;
    //[SerializeField]
    //private List<Transform> startingPoints;
    //[SerializeField]
    //private List<LayerMask> playerLayers;

    [SerializeField] private TextMeshProUGUI welcomeMessage;

    //private PlayerInputManager playerInputManager;

    private void Awake()
    {
        PlayersLoader.LoadAllPlayers(ref players);
        if (players== null)
        {
            players = new List<Player>();
        }
        
        PlayersLoader.LoadSelected(ref players, ref selectedPlayer);
        if (selectedPlayer == null)
        {
            selectedPlayer = new Player();
        }
        welcomeMessage.text = "Welcome " + selectedPlayer.Name +"!";
        //playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    /*private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        playerInputs.Add(player);

    }*/
}