using Car;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    public static SpeedSlider instance;

    [SerializeField] private Slider speedSlider; // Reference to your Slider UI element
    private Player player; // Reference to your Player script
    private float _speed;

    private void Start()
    {
        // Initialize the slider value to the player's initial speed
        speedSlider.value = 5;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public float GetSpeed()
    { 
        return speedSlider.value;
    }
}
