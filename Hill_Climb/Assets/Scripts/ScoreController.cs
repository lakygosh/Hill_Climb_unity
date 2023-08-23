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
    public Vector2 distance;
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
    
    
    public void saveScore(ref TextMeshProUGUI score)
    {
        if(distance.x>player.playerData.BestScore)
        {
            player.playerData.BestScore = distance.x;
            Debug.Log(player.playerData.BestScore);
            StartCoroutine(PlayerManager.SaveScore(player));
            score.color = Color.green;
            score.text = "New Best Score: " + distance.x.ToString("F0") + " m";
        }
        else
        {
            score.text = "Score: " + instance.distance.x.ToString("F0") + " m";
        }

    }
    
    
}
