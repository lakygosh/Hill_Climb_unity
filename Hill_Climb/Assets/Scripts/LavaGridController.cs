using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGridController : MonoBehaviour
{
    [SerializeField] internal Transform _lava;
    private Transform _tempLava;
    private int _count;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            if (GameManager.IsGameOver)
            {
                GameManager.instance.GameOver();
            }
            
        }
    }
    void Start()
    {
        _count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LavaGenerator(Vector3 lavaPos)
    {
            _tempLava = Instantiate(_lava, lavaPos, Quaternion.identity);
            _count++;
    }
}
