using System.Collections;
using System.Collections.Generic;
using Car;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _distanceText;
    private Transform _playerTrans;
    private Player _player;
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
        _player = PlayerSpawner.spawnedObject.GetComponent<Player>();
        _playerTrans = _player.transform;
        _startPosition = _playerTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsGameOver)
        {
            distance = (Vector2)_playerTrans.position - _startPosition;
            distance.y = 0f;

            if (distance.x < 0f)
            {
                distance.x = 0f;
            }

            _distanceText.text = distance.x.ToString("F0") + " m";
        }
    }
    
    
    public void saveScore(ref TextMeshProUGUI score)
    {
        if(distance.x>PlayerManager.GetSelectedPlayer().playerData.BestScore)
        {
            PlayerManager.GetSelectedPlayer().playerData.BestScore = distance.x;
            Debug.Log(_player.playerData.BestScore);
            StartCoroutine(PlayerManager.SaveScore(PlayerManager.GetSelectedPlayer()));
            score.color = Color.green;
            score.text = "New Best Score: " + distance.x.ToString("F0") + " m";
        }
        else
        {
            score.text = "Score: " + instance.distance.x.ToString("F0") + " m";
        }

    }
    
    
}
