using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [SerializeField] EnviromentGeneratorOpustanje instance;
    // Start is called before the first frame update
    void Start()
    {
        EnviromentGeneratorOpustanje.instance = Instantiate(instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
