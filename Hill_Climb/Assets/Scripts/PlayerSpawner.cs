using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Car;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> cars;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public static GameObject spawnedObject;
    // Start is called before the first frame update
    void Awake()
    {
        string numericPart = Regex.Match(PlayerManager.GetSelectedPlayer().playerData.SelectedCar, @"\d+").Value;
        int index = Int32.Parse(numericPart) - 1;
        Debug.Log(cars[index]);
        spawnedObject = Instantiate(cars[index], new Vector3(10,15,-15), Quaternion.identity);
        Debug.Log(spawnedObject);
        virtualCamera.Follow = spawnedObject.transform;
        virtualCamera.LookAt = spawnedObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
