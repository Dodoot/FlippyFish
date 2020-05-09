using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    // const string MASTER_VOLUME_KEY = "master volume";
    const string SOUND_ON_KEY = "sound on";
    const string DIFFICULTY_KEY = "difficulty";
    const string BEST_SCORE_EASY_KEY = "best score easy";
    const string BEST_SCORE_NORMAL_KEY = "best score normal";
    const string BEST_SCORE_HARD_KEY = "best score hard";

    // const float MIN_VOLUME = 0f;
    // const float MAX_VOLUME = 1f;

    // public static float GetMasterVolume() { return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY); }
    public static int GetSoundOn() { return PlayerPrefs.GetInt(SOUND_ON_KEY, 1); }
    public static int GetDifficulty() { return PlayerPrefs.GetInt(DIFFICULTY_KEY, 1); }

    public static int GetBestScore() {
        int difficultySetting = GetDifficulty();

        if(difficultySetting == 0)
        {
            return PlayerPrefs.GetInt(BEST_SCORE_EASY_KEY);
        }
        else if (difficultySetting == 1)
        {
            return PlayerPrefs.GetInt(BEST_SCORE_NORMAL_KEY);
        }
        else if (difficultySetting == 2)
        {
            return PlayerPrefs.GetInt(BEST_SCORE_HARD_KEY);
        }
        else { return -1; }
    }

    // public static void SetMasterVolume(float volume)
    // {
    //     if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
    //     {
    //         PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    //     }
    //     else
    //     {
    //         Debug.LogError("Master volume is out of range");
    //     }
    // }

    public static void SwitchSoundOn()
    {
        if(GetSoundOn() == 0)
        {
            PlayerPrefs.SetInt(SOUND_ON_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(SOUND_ON_KEY, 0);
        }
    }

    public static void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
    }

    public static void SetBestScore(int newBestScore)
    {
        int difficultySetting = GetDifficulty();

        if (difficultySetting == 0)
        {
            PlayerPrefs.SetInt(BEST_SCORE_EASY_KEY, newBestScore);
        }
        else if (difficultySetting == 1)
        {
            PlayerPrefs.SetInt(BEST_SCORE_NORMAL_KEY, newBestScore);
        }
        else if (difficultySetting == 2)
        {
            PlayerPrefs.SetInt(BEST_SCORE_HARD_KEY, newBestScore);
        }
        
    }
}
