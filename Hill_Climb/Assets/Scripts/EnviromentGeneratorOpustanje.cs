using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Car;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class EnviromentGeneratorOpustanje : MonoBehaviour
{
    private const float CAR_DISTANCE_SPAWN_MAP = 300f;
    public static EnviromentGeneratorOpustanje instance;
    [SerializeField] private SpriteShapeController _spriteShapeController;
    private Player _player;

    private int _levelLength = 2;
    [SerializeField, Range(1f, 50f)] private float _xMultiplier;
    [SerializeField, Range(1f, 50f)] private float _yMultiplier;
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness;

    [SerializeField] private float _noiseStep;
    private float _bottom = 50f;

    private Vector3 _lastLvlFragmentPos; // memorize the last position and create a relative curve based on that position
    private int i = 0;
    private int count = 0;
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //}
    private void Start()
    {
        //Remove all existing points in the spline

        int pointCount = _spriteShapeController.spline.GetPointCount();
        //for (int p = pointCount - 1; p >= 0; p--)
        //{
        //    _spriteShapeController.spline.RemovePointAt(p);
        //}

        //_lastLvlFragmentPos = transform.position;
        //_lastLvlFragmentPos = _spriteShapeController.spline.GetPosition(pointCount - 4);

        _player = PlayerSpawner.spawnedObject.GetComponent<Player>();
    }

    //private void OnValidate()
    //{
    //    _noiseStep = Random.value;
    //    // _spriteShapeController.spline.Clear();

    //    for (i = 0; i < _levelLength; i++)
    //    {
    //        _lastLvlFragmentPos = _lastLvlFragmentPos + new Vector3(_xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier); // transform.position returns the position of the object

    //        _spriteShapeController.spline.InsertPointAt(i, _lastLvlFragmentPos); // Create a point on the terrain with index i and location _lastPos

    //        if (i != 0 && i != _levelLength - 1)
    //        {
    //            _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous); // Prevent spiky terrain
    //            _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
    //            _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness); // Left and right tangent through the spike of the hill, basically stretching the terrain and making a nice curve
    //        }
    //    }

    //    _spriteShapeController.spline.InsertPointAt(_levelLength, new Vector3(_lastLvlFragmentPos.x, transform.position.y - _bottom));

    //    _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
    //}

    private void Update()
    {
        if (Vector3.Distance(_lastLvlFragmentPos, _player.GetPosition()) < CAR_DISTANCE_SPAWN_MAP)
        {
            count++;
            MapRender();
        }
    }

    private void MapRender()
    {
        //_yMultiplier = _yMultiplier + 0.5f;
        int j = 0;
        while (j < _levelLength)
        {
            _lastLvlFragmentPos = transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier); // transform.position returns the position of the object

            _spriteShapeController.spline.InsertPointAt(i, _lastLvlFragmentPos); // Create a point on the terrain with index i and location _lastPos   

            _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous); // Prevents spiky terrain
            _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * (_xMultiplier * _curveSmoothness));
            _spriteShapeController.spline.SetRightTangent(i, Vector3.right * (_xMultiplier * _curveSmoothness)); // Left and right tangent through the spike of the hill, basically stretching the terrain and making a nice curve

            i++;
            j++;
        }
        if (i > 0)
        {
            _spriteShapeController.spline.InsertPointAt(i, new Vector3(_lastLvlFragmentPos.x, transform.position.y - _bottom));

            _spriteShapeController.spline.InsertPointAt(i + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
        }
        _noiseStep = Random.value;

    }

}
