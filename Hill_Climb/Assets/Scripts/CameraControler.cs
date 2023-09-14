using Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public float minHeight = 0f;  // Minimum Y-axis position
    public float maxHeight = 10f; // Maximum Y-axis position
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        //// Get the current camera position
        //Vector3 currentPosition = transform.position;

        //// Clamp the Y-axis position within the defined limits
        //currentPosition.y = Mathf.Clamp(currentPosition.y, minHeight, maxHeight);

        //// Update the camera's position
        //transform.position = currentPosition;

        if (_player != null)
        {
            // Get the current camera position

            Vector3 currentPosition = transform.position;
            if (currentPosition.y < minHeight)
            {
                currentPosition.y = minHeight;
            }
            else
            {
                // Ensure the camera stays above the minimum height
                currentPosition.y = Mathf.Max(currentPosition.y, minHeight);
            }

            // Set the camera's position to follow the player horizontally
            currentPosition.x = _player.GetPosition().x;

            // Update the camera's position
            transform.position = currentPosition;
        }
    }
}
