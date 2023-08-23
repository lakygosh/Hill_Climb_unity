using System.Collections;
using System.Collections.Generic;
using Car;
using UnityEngine;
using UnityEngine.U2D;

public class BackgroundController : MonoBehaviour
{
    private const float BACKGROUND_FRAGMENT_DISSTANCE = 100f;

    public static BackgroundController instance;
    Transform camera;

    private Vector3 _lastBackgroundFragmentPos;
    private int i = 0;

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
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    { 

        transform.position = new Vector3(camera.position.x, camera.position.y, 0);


    }

}