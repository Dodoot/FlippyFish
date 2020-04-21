using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadStartMenu()
    {
        Time.timeScale = 1;
        FindObjectOfType<GameSession>().ResetCurrentScore();
        SceneManager.LoadScene("Menu - Start");
    }
  
    public void LoadGame()
    {
        Time.timeScale = 1;
        FindObjectOfType<GameSession>().ResetCurrentScore();
        SceneManager.LoadScene("Level 1");
    }

    public void LoadGameOver()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu - Game Over");
    }

    public void LoadCredits()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu - Credits");
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        FindObjectOfType<GameSession>().ResetCurrentScore();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
  
    public void LoadNextScene()
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
  
    public void QuitGame()
    {
        Application.Quit();
    }
}
