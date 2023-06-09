using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class EnviromentGenerator : MonoBehaviour
{
    private const float CAR_DISTANCE_SPAWN_MAP = 200f;
    private const float FUEL_DISTANCE = 200f;

    [SerializeField] private SpriteShapeController _spriteShapeController;
    [SerializeField] private Player _player;

    [SerializeField, Range(3f, 100f)] private int _levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float _xMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] private float _yMultiplier = 2f;
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;

    [SerializeField] private float _noiseStep = 0.5f;
    [SerializeField] private float _bottom = 10f;

    private Vector3 _lastLvlFragmentPos; //memorize last position and create realtive curve base on that position
    private int i = 0;

    private void OnValidate()
    {
        _spriteShapeController.spline.Clear();

       for(i = 0; i < _levelLength; i++)
        {
            _lastLvlFragmentPos = transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier); //transform.position returns position of the object
            _spriteShapeController.spline.InsertPointAt(i, _lastLvlFragmentPos); //Create point on terrain with index i and location _lastPos

            if (i != 0 && i != _levelLength - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous); //Prevents spiky terrain
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);//Left and right tangent through the spike of the hill, basicly streching the terrain and making nice curve
            }
        }

        _spriteShapeController.spline.InsertPointAt(_levelLength, new Vector3(_lastLvlFragmentPos.x, transform.position.y - _bottom));

        _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new Vector3(transform.position.x, transform.position.y - _bottom));

    }

    private void MapRender() 
    {
        int j = 0;
        while (j < _levelLength)
        {
            _lastLvlFragmentPos = transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier); //transform.position returns position of the object
            _spriteShapeController.spline.InsertPointAt(i, _lastLvlFragmentPos); //Create point on terrain with index i and location _lastPos   

           // if (i != 0 && i != _levelLength - 1)
           //  {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous); //Prevents spiky terrain
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);//Left and right tangent through the spike of the hill, basicly streching the terrain and making nice curve
           // }
            i++;
            j++;
        }

        _spriteShapeController.spline.InsertPointAt(i, new Vector3(_lastLvlFragmentPos.x, transform.position.y - _bottom));

        _spriteShapeController.spline.InsertPointAt(i + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
    }

    private void Update()
    {

        if (Vector3.Distance(_lastLvlFragmentPos, _player.GetPosition()) < CAR_DISTANCE_SPAWN_MAP)
        { 
            //render map part
            MapRender();
        }
    }
}
