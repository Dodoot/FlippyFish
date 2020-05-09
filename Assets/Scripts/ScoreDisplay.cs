using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] bool isBestScore = false;
    [SerializeField] bool isDifficultyLevel = false;

    TextMeshProUGUI scoreText;
    GameSession gameSession;
    int bestScore = 0;
    int difficultySetting = 0;
    string difficultySettingName = "NOT DEFINED";

    // Unity methods
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        bestScore = PlayerPrefsController.GetBestScore();

        difficultySetting = PlayerPrefsController.GetDifficulty();
        if(difficultySetting == 0) { difficultySettingName = "Calm"; }
        else if (difficultySetting == 1) { difficultySettingName = "Normal"; }
        else { difficultySettingName = "Fishy"; }
    }
    
    void Update()
    {
        if (isDifficultyLevel) { scoreText.text = difficultySettingName; }
        else if (isBestScore) { scoreText.text = bestScore.ToString(); }
        else { scoreText.text = gameSession.GetCurrentScore().ToString(); }
    }
}
