using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty Level")]
public class DifficultyLevel : ScriptableObject
{
    // Config params
    [SerializeField] EnemiesPattern[] enemiesPatterns;
    [SerializeField] float timeBetweenPatternsMinSeconds = 1f;
    [SerializeField] float timeBetweenPatternsMaxSeconds = 1f;
    [SerializeField] float gameSpeed = 1f;

    // Get methods
    public EnemiesPattern[] GetEnemiesPatterns() { return enemiesPatterns; }
    public float GetTimeBetweenPatternsMinSeconds() { return timeBetweenPatternsMinSeconds; }
    public float GetTimeBetweenPatternsMaxSeconds() { return timeBetweenPatternsMaxSeconds; }
    public float GetGameSpeed() { return gameSpeed; }
}
