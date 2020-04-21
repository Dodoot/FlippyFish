using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string BEST_SCORE_KEY = "best score";

    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;

    public static float GetMasterVolume() { return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY); }
    public static float GetDifficulty() { return PlayerPrefs.GetFloat(DIFFICULTY_KEY); }
    public static int GetBestScore() { return PlayerPrefs.GetInt(BEST_SCORE_KEY); }

    public static void SetMasterVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }

    public static void SetDifficulty(float difficulty)
    {
        PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
    }

    public static void SetBestScore(int newBestScore)
    {
        PlayerPrefs.SetInt(BEST_SCORE_KEY, newBestScore);
    }
}
