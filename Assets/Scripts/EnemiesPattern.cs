using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ennemies Pattern")]
public class EnemiesPattern : ScriptableObject
{
    // Config params
    [SerializeField] Enemy[] enemiesPrefabs;
    [SerializeField] int[] enemiesPositions;  // 2 is on player, 3 is next to player, 4 is 3/4 1/4.
    [SerializeField] float[] timesBetweenEnemies;

    // Get methods
    public Enemy[] GetEnemiesPrefabs() { return enemiesPrefabs; }
    public int[] GetEnemiesPositions() { return enemiesPositions; }
    public float[] GetTimesBetweenEnemies() { return timesBetweenEnemies; }
}
