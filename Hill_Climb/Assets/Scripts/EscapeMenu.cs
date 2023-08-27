using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public void Continue ()
    {
        if (GameManager._escapeMenuOn)
        {
            GameManager._escapeMenuOn = false;
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("EscapeMenu");
        }
    }
    
    public void Options () 
    {
        
    }
    
    public void MainMenu () 
    {
        GameManager._escapeMenuOn = false;
        SceneManager.LoadScene("Background");
        
    }


    private void Update()
    {
        Continue();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SinglePlayer");
    }
}
