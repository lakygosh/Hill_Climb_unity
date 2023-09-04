using Car;
using UnityEngine;

public class CollectFuel : MonoBehaviour
{
    public static CollectFuel instance;

    [SerializeField] private Transform _fuelCanister;
    private Player _player;

    private Vector3 _lastFuelPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fuel"))
        {
            if (!GameManager.IsGameOver)
            {
                FuelController.instance.FillFuel();
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

    }

    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerSpawner.spawnedObject.GetComponent<Player>();
        _lastFuelPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_lastFuelPosition, _player.transform.position) <= 100f)
        {
            _lastFuelPosition = FuelGenerator(_lastFuelPosition + Vector3.right * 600f);//+ Vector3.up * (enviromentGenerator.transform.position.y));
        }
    }

    public Vector3 FuelGenerator(Vector3 fuelPositon)
    {
        Instantiate(_fuelCanister, fuelPositon, Quaternion.identity);
        return fuelPositon;
    }
}
