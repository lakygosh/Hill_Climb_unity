using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.25f;
    public Vector3 offset;

    public float minHeight = 0f;  // Minimum Y-axis position
    private Player _player;

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        if (currentPosition.y < minHeight)
        {
            currentPosition.y = minHeight;
           
         
            transform.position = currentPosition;
        }
        else
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        
        //_frontTireSprite.material = _material;
        //_backTireSprite.material = _material;
        //_carSprite.material = _material;
    }
}
