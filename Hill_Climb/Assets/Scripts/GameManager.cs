using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool _escapeMenuOn = false;

    public static bool EscapeMenuOn
    {
        get => _escapeMenuOn;
        set => _escapeMenuOn = value;
    }

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
        TextMeshProUGUI score = _gameOverCanvas.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(score);
        ScoreController.instance.saveScore(ref score);
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
