using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // game settings
    [SerializeField] int myTargetFramerate = 60;
    [SerializeField] int[] difficultyThresholds;

    // variables
    int currentScore = 0;
    int currentThreshold = 0;

    public int GetCurrentScore() { return currentScore; }

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }

    void Start()
    {
        Application.targetFrameRate = myTargetFramerate;
    }

    public void AddToCurrentScore(int scoreValue)
    {
        currentScore += scoreValue;

        if (currentThreshold < difficultyThresholds.Length)
        {
            CheckScoreThreshold();
        }
    }

    private void CheckScoreThreshold()
    {
        if (currentScore >= difficultyThresholds[currentThreshold])
        {
            FindObjectOfType<EnemiesManager>().DifficultyUp();
            currentThreshold += 1;
        }
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
        currentThreshold = 0;
    }

    public void SetBestScore()
    {
        int previousBestScore = PlayerPrefsController.GetBestScore();
        int newBestScore = Mathf.Max(previousBestScore, currentScore);
        PlayerPrefsController.SetBestScore(newBestScore);
    }
}
