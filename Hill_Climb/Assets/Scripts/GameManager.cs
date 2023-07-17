using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool _escapeMenuOn = false;

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _gamePauseCanvas;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = 1.0f;
    }

    public void GamePaused()
    {
        _gamePauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        //Time.timeScale = 0.5f;
        Thread.Sleep(5000);
        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_escapeMenuOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("EscapeMenu", LoadSceneMode.Additive);
                Time.timeScale = 0;
                _escapeMenuOn = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.UnloadSceneAsync("EscapeMenu");
                Time.timeScale = 1;
                _escapeMenuOn = false;
            }
        }
        
    }
}
