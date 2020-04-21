using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] bool isBestScore = false;

    TextMeshProUGUI scoreText;
    GameSession gameSession;
    int bestScore = 0;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        bestScore = PlayerPrefsController.GetBestScore();
    }
    
    void Update()
    {
        if (isBestScore) { scoreText.text = bestScore.ToString(); }
        else { scoreText.text = gameSession.GetCurrentScore().ToString(); }
    }
}
