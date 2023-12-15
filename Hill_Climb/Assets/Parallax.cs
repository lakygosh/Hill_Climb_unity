using Car;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal.XR.WSA;

public class Parallax : MonoBehaviour
{
    Material material;
    float distance;
    Transform camera;
    //[Range(0f, 0.5f)]
    private float speed { get;  set; }
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;

        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(camera.position.x, transform.position.y, 10);

        //speed = (float)Math.Log10(SpeedCalculator.instance.speed);
        speed = SpeedCalculator.instance.speed / 100f;
        //speed = 0.2f;

        distance += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
