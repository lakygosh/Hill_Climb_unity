using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampController : MonoBehaviour
{

    [SerializeField] internal Transform _ramp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RampGenerator(Vector3 rampPos)
    {
        Instantiate(_ramp, rampPos, Quaternion.identity);        
    }
}
