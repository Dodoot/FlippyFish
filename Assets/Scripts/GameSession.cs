using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // game settings
    [SerializeField] int myTargetFramerate = 60;

    // variables
    int currentScore = 0;
    int currentThresholdIndex = 0;
    int nextThreshold = 0;

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
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void AddToCurrentScore(int scoreValue)
    {
        currentScore += scoreValue;
        CheckScoreThreshold();
    }

    private void CheckScoreThreshold()
    {
        if(nextThreshold == 0)
        {
            int[] difficultyThresholds = FindObjectOfType<DifficultyManager>().GetDifficultyThresholds();
            nextThreshold = difficultyThresholds[currentThresholdIndex];
        }

        if (currentScore >= nextThreshold)
        {
            FindObjectOfType<EnemiesManager>().DifficultyUp();

            currentThresholdIndex += 1;

            int[] difficultyThresholds = FindObjectOfType<DifficultyManager>().GetDifficultyThresholds();
            if(currentThresholdIndex < difficultyThresholds.Length)
            {
                nextThreshold = difficultyThresholds[currentThresholdIndex];
            }
            else
            {
                Debug.Log("Max difficulty reached");
                nextThreshold = 1000; // This is very big !
            }
        }
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
        currentThresholdIndex = 0;
        nextThreshold = 0;
    }

    public void SetBestScore()
    {
        int previousBestScore = PlayerPrefsController.GetBestScore();
        int newBestScore = Mathf.Max(previousBestScore, currentScore);
        PlayerPrefsController.SetBestScore(newBestScore);
    }
}
