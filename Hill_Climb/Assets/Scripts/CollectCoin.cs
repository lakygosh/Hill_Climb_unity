using Car;
using System;
using TMPro;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
	public static CollectCoin instance;

    [SerializeField] private Transform _coin;
    private Player player;

    private Vector3 _lastCoinPosition;

    private float _yOffset = 2f; // Offset from the terrain

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

    private void OnValidate()
    {
        //// Get the terrain height at the X position of the coin
        //float terrainHeight = GetTerrainHeightAtPosition(transform.position.x);

        //// Calculate the coin's position above the terrain
        //Vector3 coinPosition = transform.position;
        //coinPosition.y = terrainHeight + _yOffset;


        //transform.position = coinPosition;
        //_lastCoinPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerSpawner.spawnedObject.GetComponent<Player>();
        float terrainHeight = GetTerrainHeightAtPosition(_lastCoinPosition.x);

        // Calculate the coin's initial position above the terrain
        _lastCoinPosition.y = terrainHeight + _yOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_lastCoinPosition, player.transform.position) <= 40f)
        {
            // Get the terrain height at the X position of the coin
            float terrainHeight = GetTerrainHeightAtPosition(_lastCoinPosition.x + 100f);

            // Calculate the coin's position above the terrain

            _lastCoinPosition.y = terrainHeight + _yOffset;
            _lastCoinPosition.x += 100f;

            // Set the new coin position
            //transform.position = coinPosition;


            //_lastCoinPosition = CoinGenerator(_lastCoinPosition + Vector3.right * 600f);
            CoinGenerator(_lastCoinPosition);
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
            return 0f;
        }
    }
}
