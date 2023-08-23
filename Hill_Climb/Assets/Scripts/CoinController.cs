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
	[SerializeField]
	private Player player;
	[SerializeField] private Image _coinImage;
	[SerializeField] private TextMeshProUGUI _coinText;


	private void Awake()
    {
        if (instance == null) 
        {
            instance = this;   
        }
        _coinText.text = player.playerData.Coins.ToString();

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
		player.playerData.Coins += 100;
		StartCoroutine(PlayerManager.SaveCoins(player));
		_coinText.text = player.playerData.Coins.ToString();
	}
}
