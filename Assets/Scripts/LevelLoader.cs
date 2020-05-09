using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingCanvas;

    private void Start()
    {
        loadingCanvas.SetActive(false);
    }

    public void LoadStartMenu()
    {
        loadingCanvas.SetActive(true);
        Time.timeScale = 1;
        FindObjectOfType<GameSession>().ResetCurrentScore();
        SceneManager.LoadScene("Menu - Start");
    }
  
    public void LoadGame()
    {
        loadingCanvas.SetActive(true);
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
        loadingCanvas.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu - Credits");
    }

    public void ReloadScene()
    {
        loadingCanvas.SetActive(true);
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
