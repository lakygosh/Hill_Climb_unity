using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform camera; //main camera
    Vector3 camStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] materials;
    float[] backSpeeds;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        camStartPos = camera.position;

        int backCount = transform.childCount;
        materials = new Material[backCount];
        backSpeeds = new float[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            materials[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((backgrounds[i].transform.position.z - camera.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - camera.position.z;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            backSpeeds[i] = 1 - (backgrounds[i].transform.position.z - camera.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = camera.position.x - camStartPos.x;
        transform.position = new Vector3(camera.position.x, transform.position.y, 10);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeeds[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
