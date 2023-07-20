using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    [SerializeField] private SpriteRenderer _frontTireSprite;
    [SerializeField] private SpriteRenderer _backTireSprite;
    [SerializeField] private SpriteRenderer _carSprite;
    [SerializeField] private Material _material;

    private void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        //_frontTireSprite.material = _material;
        //_backTireSprite.material = _material;
        //_carSprite.material = _material;
    }
}
