using System.Collections;
using System.Collections.Generic;
using Car;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public static BackgroundController instance;

    [SerializeField] private Transform _backgroundFragment;
    Transform _camera;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_camera.position.x, _camera.position.y, 0);
    }

}