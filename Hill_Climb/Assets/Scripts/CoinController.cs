using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinController : MonoBehaviour
{

	public static CoinController instance;
	
	[SerializeField] private Image _coinImage;
	[SerializeField] private float currCoinNumber;
	[SerializeField] private TextMeshProUGUI _coinText;


	private void Awake()
    {
        if (instance == null) 
        {
            instance = this;   
        }
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
		currCoinNumber = currCoinNumber + 100;
		_coinText.text = currCoinNumber.ToString();
	}
}
