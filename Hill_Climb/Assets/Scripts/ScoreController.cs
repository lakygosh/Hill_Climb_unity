using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private Transform _playerTrans;

    private Vector2 _startPosition;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _playerTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = (Vector2)_playerTrans.position - _startPosition;
        distance.y = 0f;

        if (distance.x < 0f)
        { 
            distance.x = 0f;
        }    

        _distanceText.text = distance.x.ToString("F0") + " m";
    }
}
