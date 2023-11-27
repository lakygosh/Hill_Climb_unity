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
public class EnviromentGeneratorKontrakcija : MonoBehaviour
{
    private const float CAR_DISTANCE_SPAWN_MAP = 300f;

    [SerializeField] private SpriteShapeController _spriteShapeController;
    private Player _player;
    //[SerializeField] private LavaGridController _lavaController;
    [SerializeField] private LavaGridController _lavaController;

    private int _levelLength = 2;
    private float _xMultiplier = 21f;
    [SerializeField, Range(1f, 50f)] private float _yMultiplier = 2.1f;
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;

    [SerializeField] private float _noiseStep = 0.5f;
    private float _bottom = 50f;

    private Vector3 _lastLvlFragmentPos; // memorize the last position and create a relative curve based on that position
    private int i = 0;
    private int _mapRednerCounter = 0;

    private void Start()
    {
        //_player = PlayerManager.GetSelectedPlayer();

        _player = PlayerSpawner.spawnedObject.GetComponent<Player>();

    }

    private void OnValidate()
    {
        //_levelLength = Random.Range(40, 80);
        //_xMultiplier = Random.Range(20, 22) + Random.value * Random.Range(22, 30);
        //_yMultiplier = Random.Range(10, 12) + Random.value * Random.Range(15, 22);
        _noiseStep = Random.value;
        _spriteShapeController.spline.Clear();

        for (i = 0; i < _levelLength; i++)
        {
            _lastLvlFragmentPos = transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier); // transform.position returns the position of the object

            _spriteShapeController.spline.InsertPointAt(i, _lastLvlFragmentPos); // Create a point on the terrain with index i and location _lastPos

            if (i != 0 && i != _levelLength - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous); // Prevent spiky terrain
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness); // Left and right tangent through the spike of the hill, basically stretching the terrain and making a nice curve
            }
        }

        _spriteShapeController.spline.InsertPointAt(_levelLength, new Vector3(_lastLvlFragmentPos.x, transform.position.y - _bottom));

        _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
    }

    private void Update()
    {
        if (Vector3.Distance(_lastLvlFragmentPos, _player.GetPosition()) < CAR_DISTANCE_SPAWN_MAP)
        {
            if (_mapRednerCounter == 3)
            {
                FlatGroundRender();
                _mapRednerCounter = 0;
            }
            else
            {
                MapRender();
                _mapRednerCounter++;
            }
        }
    }

    private void FlatGroundRender()
    {
        int j = 0;
        while (j < 3)
        {
            _lastLvlFragmentPos = transform.position + new Vector3(i * _xMultiplier, _lastLvlFragmentPos.y);

            if (j == 1)
            {
                float _currentheight = _lastLvlFragmentPos.y;
                _lastLvlFragmentPos += new Vector3(0, -_bottom);

                _spriteShapeController.spline.InsertPointAt(i, _lastLvlFragmentPos);

                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * (100f * _curveSmoothness));
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * (100f * _curveSmoothness));



                //_spriteShapeController.spline.InsertPointAt(i, _lastLvlFragmentPos);

                //_spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                //_spriteShapeController.spline.SetLeftTangent(i, Vector3.left * (_xMultiplier * _curveSmoothness));
                //_spriteShapeController.spline.SetRightTangent(i, Vector3.right * (_xMultiplier * _curveSmoothness));
                _lavaController.LavaGenerator(new Vector3(_lastLvlFragmentPos.x - 380f, _lastLvlFragmentPos.y + 50f, 0));

                _lastLvlFragmentPos.y = _currentheight;

            }
            else
            {
                //_spriteShapeController.spline.InsertPointAt(i, _lastLvlFragmentPos);

                //_spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                //_spriteShapeController.spline.SetLeftTangent(i, Vector3.left * (60f * _curveSmoothness));
                //_spriteShapeController.spline.SetRightTangent(i, Vector3.right * (60f * _curveSmoothness));

                _spriteShapeController.spline.InsertPointAt(i, _lastLvlFragmentPos);

                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * (_xMultiplier * _curveSmoothness));
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * (_xMultiplier * _curveSmoothness));

            }

            i++;
            j++;
        }

            _spriteShapeController.spline.InsertPointAt(i, new Vector3(_lastLvlFragmentPos.x, transform.position.y - _bottom));

            //_spriteShapeController.spline.InsertPointAt(i + 1, new Vector3(transform.position.x, transform.position.y - _bottom));

    }

    private void MapRender()
    {
        _yMultiplier = _yMultiplier + 0.5f;
        _noiseStep = Random.value;
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

        _spriteShapeController.spline.InsertPointAt(i, new Vector3(_lastLvlFragmentPos.x, transform.position.y - _bottom));

        //_spriteShapeController.spline.InsertPointAt(i + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
    }


}
