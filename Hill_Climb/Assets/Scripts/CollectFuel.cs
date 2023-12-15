using Car;
using UnityEngine;

public class CollectFuel : MonoBehaviour
{
    public static CollectFuel instance;

    [SerializeField] private Transform _fuelCanister;
    private Player _player;

    private float _yOffset = 2f;
    private Vector3 _lastFuelPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fuel"))
        {
            SFXController.instance.FuelCollect();

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
        _lastFuelPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerSpawner.spawnedObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(GetTerrainHeightAtPosition(_lastFuelPosition.x + 200f) == -1f))
        {
            if (Vector3.Distance(_lastFuelPosition, _player.transform.position) <= 40f)
            {
                // Get the terrain height at the X position of the coin
                float terrainHeight = GetTerrainHeightAtPosition(_lastFuelPosition.x + 200f);

                // Calculate the coin's position above the terrain

                _lastFuelPosition.y = terrainHeight + _yOffset;
                _lastFuelPosition.x += 200f;

                FuelGenerator(_lastFuelPosition);
            }
        }
    }

    public void FuelGenerator(Vector3 fuelPositon)
    {
        Instantiate(_fuelCanister, fuelPositon, Quaternion.identity);
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

