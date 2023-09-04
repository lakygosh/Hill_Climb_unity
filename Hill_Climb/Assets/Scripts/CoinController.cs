using System.Collections;
using System.Collections.Generic;
using Car;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinController : MonoBehaviour
{
	public static CoinController instance;
	private Player _player;
	[SerializeField] private Image _coinImage;
	[SerializeField] private TextMeshProUGUI _coinText;


	private void Awake()
    {
        if (instance == null) 
        {
            instance = this;   
        }
	    _player = PlayerManager.GetSelectedPlayer();
        _coinText.text = _player.playerData.Coins.ToString();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void AddCoins() 
	{
		PlayerManager.GetSelectedPlayer().playerData.Coins += 100;
		StartCoroutine(PlayerManager.SaveCoins(PlayerManager.GetSelectedPlayer()));
		_coinText.text = PlayerManager.GetSelectedPlayer().playerData.Coins.ToString();
	}
}
