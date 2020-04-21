using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] float loseWaitTime = 3f;

    bool alreadyLost = false;
    bool pauseMenuOpened = false;

    public bool GetAlreadyLost() { return alreadyLost; }

    void Start()
    {
        pauseCanvas.SetActive(false);
    }

    void Update()
    {
        OpenClosePauseMenu();
    }

    private void OpenClosePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenuOpened is false)
            {
                Time.timeScale = 0.01f;
                pauseCanvas.SetActive(true);
                pauseMenuOpened = true;
            }
            else
            {
                Time.timeScale = FindObjectOfType<EnemiesManager>().GetCurrentSpeed();
                pauseCanvas.SetActive(false);
                pauseMenuOpened = false;
            }
        }
    }

    public void LoseGame()
    {
        if (alreadyLost == false)
        {
            alreadyLost = true;
            FindObjectOfType<EnemiesManager>().SetInfiniteLooping(false);
            StartCoroutine(WaitBeforeGameOver());
            FindObjectOfType<GameSession>().SetBestScore();
        }
    }

    IEnumerator WaitBeforeGameOver()
    {
        // Fade Screen to black
        yield return new WaitForSeconds(loseWaitTime);
        FindObjectOfType<LevelLoader>().LoadGameOver();
    }
}
