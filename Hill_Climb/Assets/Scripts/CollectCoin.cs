using Car;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
	public static CollectCoin instance;

	[SerializeField] private Transform _coin;
    [SerializeField] private Player player;

    private Vector3 _lastCoinPosition;

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CoinController.instance.AddCoins();
            Destroy(gameObject);
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_lastCoinPosition, player.transform.position) <= 100f)
        {
            _lastCoinPosition = CoinGenerator(_lastCoinPosition + Vector3.right * 600f);
        }
		
    }

	public Vector3 CoinGenerator(Vector3 coinPositon)
    {
        Instantiate(_coin, coinPositon, Quaternion.identity);
        return coinPositon;
    }


}
