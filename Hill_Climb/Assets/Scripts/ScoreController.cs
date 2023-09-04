using System.Collections;
using System.Collections.Generic;
using Car;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private Transform _playerTrans;
    [SerializeField] private Player player;
    public static ScoreController instance;
    internal Vector2 distance;
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;   
        }
    }

    private Vector2 _startPosition;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _playerTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = (Vector2)_playerTrans.position - _startPosition;
        distance.y = 0f;

        if (distance.x < 0f)
        { 
            distance.x = 0f;
        }    

        _distanceText.text = distance.x.ToString("F0") + " m";
    }
    
    
    public void newBestScore()
    {
        if(distance.x>player.playerData.BestScore)
        {
            player.playerData.BestScore = distance.x;
            Debug.Log(player.playerData.BestScore);
            StartCoroutine(PlayerManager.SaveBestScore(player));
        }
    }
    
    
}
