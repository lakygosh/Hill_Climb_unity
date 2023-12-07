using Autodesk.Fbx;
using Car;
using UnityEngine;

public class SpeedCalculator : MonoBehaviour
{
    public static SpeedCalculator instance;
    private Vector3 previousPosition;
    public float speed;

    void Start()
    {
        // Initialize the previousPosition with the initial position of the object
        previousPosition = transform.position;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        // Calculate the displacement since the last frame
        Vector3 displacement = transform.position - previousPosition;

        // Calculate the speed based on the displacement and the time elapsed
        speed = displacement.magnitude / Time.deltaTime;

        // Update the previousPosition for the next frame
        previousPosition = transform.position;
    }
}
