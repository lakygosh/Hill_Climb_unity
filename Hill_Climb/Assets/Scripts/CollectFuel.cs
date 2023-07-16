using Car;
using UnityEngine;

public class CollectFuel : MonoBehaviour
{
    public static CollectFuel instance;

    [SerializeField] private Transform _fuelCanister;
    [SerializeField] private Player _player;

    private Vector3 _lastFuelPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FuelController.instance.FillFuel();
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _lastFuelPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_lastFuelPosition, _player.transform.position) <= 100f)
        {
            _lastFuelPosition = FuelGenerator(_lastFuelPosition + Vector3.right * 600f);
        }
    }

    public Vector3 FuelGenerator(Vector3 fuelPositon) //ne radi
    {
        Instantiate(_fuelCanister, fuelPositon, Quaternion.identity);
        return fuelPositon;
    }
}
