using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGridController : MonoBehaviour
{
    [SerializeField] internal Transform _lava;
    internal GameManager gameManager;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            if (!GameManager.IsGameOver)
            {
                gameManager.GameOver();
            }
            
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LavaGenerator(Vector3 lavaPos)
    {
        Instantiate(_lava, lavaPos, Quaternion.identity);
    }
}
