using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Easy")]
    [SerializeField] int[] easyDifficultyThresholds;
    [SerializeField] DifficultyLevel[] easyDifficultyLevels;

    [Header("Normal")]
    [SerializeField] int[] normalDifficultyThresholds;
    [SerializeField] DifficultyLevel[] normalDifficultyLevels;

    [Header("Hard")]
    [SerializeField] int[] hardDifficultyThresholds;
    [SerializeField] DifficultyLevel[] hardDifficultyLevels;

    int difficultySetting = 1;

    // Public methods
    public int[] GetDifficultyThresholds()
    {
        if (difficultySetting == 0)
        {
            Debug.Log("easy thresholds");
            return easyDifficultyThresholds;
        }
        else if (difficultySetting == 1)
        {
            Debug.Log("normal thresholds");
            return normalDifficultyThresholds;
        }
        else if (difficultySetting == 2)
        {
            Debug.Log("hard thresholds");
            return hardDifficultyThresholds;
        }
        else
        {
            Debug.LogError("Difficulty Setting is wrong, setting to normal difficulty");
            PlayerPrefsController.SetDifficulty(1);
            return normalDifficultyThresholds;
        }
    }

    public DifficultyLevel[] GetDifficultyLevels()
    {
        difficultySetting = PlayerPrefsController.GetDifficulty();

        if (difficultySetting == 0)
        {
            Debug.Log("easy levels");
            return easyDifficultyLevels;
        }
        else if (difficultySetting == 1)
        {
            Debug.Log("normal levels");
            return normalDifficultyLevels;
        }
        else if (difficultySetting == 2)
        {
            Debug.Log("hard levels");
            return hardDifficultyLevels;
        }
        else
        {
            Debug.LogError("Difficulty Setting is wrong, setting to normal difficulty");
            PlayerPrefsController.SetDifficulty(1);
            return normalDifficultyLevels;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        difficultySetting = PlayerPrefsController.GetDifficulty();
        Debug.Log(difficultySetting.ToString());
    }
}
