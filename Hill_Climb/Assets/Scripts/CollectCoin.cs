using Car;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = System.Random;

public class CollectCoin : MonoBehaviour
{
	public static CollectCoin instance;

    [SerializeField] private Transform _coin;
    private Player player;

    private Vector3 _lastCoinPosition;

    private float _yOffset = 2f; // Offset from the terrain

    private Random random = new Random();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            SFXController.instance.CoinCollect();
            if (!GameManager.IsGameOver)
            {
                CoinController.instance.AddCoins();
            }
            Destroy(collision.gameObject);
        }
    }

	private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _lastCoinPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerSpawner.spawnedObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(GetTerrainHeightAtPosition(_lastCoinPosition.x + 100f) == -1f))
        {
            if (Vector3.Distance(_lastCoinPosition, player.transform.position) <= 90f)
            {
                // Get the terrain height at the X position of the coin
                float terrainHeight = GetTerrainHeightAtPosition(_lastCoinPosition.x + 100f);

                // Calculate the coin's position above the terrain
                _lastCoinPosition.y = terrainHeight + _yOffset;
                _lastCoinPosition.x += 100f;
                CoinGenerator(_lastCoinPosition);

                int r = random.Next(1, 6);
                for (int i = 0; i <= r; i++)
                {
                    terrainHeight = GetTerrainHeightAtPosition(_lastCoinPosition.x + 4f);
                    _lastCoinPosition.y = terrainHeight + _yOffset;

                    _lastCoinPosition.x += 4f;
                    CoinGenerator(_lastCoinPosition);
                }
            }
        }
		
    }

	public void CoinGenerator(Vector3 coinPositon)
    {
        Instantiate(_coin, coinPositon, Quaternion.identity);
    }

    private float GetTerrainHeightAtPosition(float xPosition)
    {
        // Perform a raycast or other method to determine terrain height at the given xPosition
        // Replace this with your actual terrain height calculation logic
        // Example raycast:
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(xPosition, 10000f), Vector2.down);

        if (hit.collider != null)
        {
            return hit.point.y;
        }
        else
        {
            // Fallback value if the raycast doesn't hit 
            return -1f;
        }
    }
}
