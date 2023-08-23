using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public void Continue ()
    {
        if (GameManager._escapeMenuOn)
        {
            SceneManager.UnloadSceneAsync("EscapeMenu");
            Time.timeScale = 1;
            GameManager._escapeMenuOn = false;
        }
    }
    
    public void Options () 
    {
        
    }
    
    public void MainMenu () 
    {
        SceneManager.LoadScene("MainMenu");
        
    }

    public void Quit()
    { 
        Application.Quit();
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
