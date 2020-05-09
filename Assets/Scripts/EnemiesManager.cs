using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    // Params
    [SerializeField] float startTimeSeconds = 3;
    [SerializeField] bool infiniteLooping = true;

    // variables
    DifficultyLevel[] difficultyLevels;

    bool firstLoop = true;
    bool emptyLoop = false;
    bool stopPattern = false;
    float hurtPauseTime = 0f;
    float emptyLoopTimeSeconds = 1f;
    int currentDifficulty = 0;
    float currentSpeed = 1f;
    
    // Public methods
    public float GetCurrentSpeed() { return currentSpeed; }

    public void SetStopPattern(bool newStopPattern) { stopPattern = newStopPattern; }
    public void SetInfiniteLooping(bool newInfiniteLooping) { infiniteLooping = newInfiniteLooping; }
    public void SetCurrentDifficulty(int newDifficulty) { currentDifficulty = newDifficulty; }

    public void TriggerEmptyLoop(float newEmptyLoopTimeSeconds)
    {
        emptyLoop = true;
        emptyLoopTimeSeconds = newEmptyLoopTimeSeconds;
    }

    public void DifficultyUp()
    {
        if (currentDifficulty < difficultyLevels.Length - 1)
        {
            currentDifficulty += 1;
            FindObjectOfType<SushichefManager>().TriggerSushichefWithPause("Faster");
            Debug.Log("Difficulty: " + currentDifficulty.ToString());
        }
        else
        {
            Debug.Log("Max Difficulty reached");
        }
    }

    // Unity methods
    IEnumerator Start()
    {
        difficultyLevels = FindObjectOfType<DifficultyManager>().GetDifficultyLevels();
        currentSpeed = difficultyLevels[currentDifficulty].GetGameSpeed();
        Time.timeScale = currentSpeed;

        while (infiniteLooping)
        {
            yield return StartCoroutine(SpawnPattern());
        }
    }

    // Private methods
    private IEnumerator SpawnPattern()
    {
        if (firstLoop is true)
        {
            yield return new WaitForSeconds(startTimeSeconds);
            firstLoop = false;
        }
        if (emptyLoop is true)
        {
            FindObjectOfType<SushichefManager>().SetReadyForTrigger(true);
            yield return new WaitForSeconds(emptyLoopTimeSeconds);
            emptyLoop = false;
        }

        currentSpeed = difficultyLevels[currentDifficulty].GetGameSpeed();
        Time.timeScale = currentSpeed;

        EnemiesPattern[] enemiesPatterns = difficultyLevels[currentDifficulty].GetEnemiesPatterns();
        
        int randomPatternIndex = Random.Range(0, enemiesPatterns.Length);
        EnemiesPattern enemyPattern = enemiesPatterns[randomPatternIndex];

        Enemy[] enemiesPrefabs = enemyPattern.GetEnemiesPrefabs();
        int[] enemiesPositions = enemyPattern.GetEnemiesPositions();
        float[] timesBetweenEnemies = enemyPattern.GetTimesBetweenEnemies();

        for (int i = 0; i < enemiesPrefabs.Length; i++)
        {
            if(stopPattern == true)
            {
                yield return new WaitForSeconds(hurtPauseTime);
                break;
            }
            yield return StartCoroutine(SpawnEnemy(enemiesPrefabs[i], enemiesPositions[i], timesBetweenEnemies[i]));
            // Debug.Log("LOOOP" + i.ToString());
        }
        stopPattern = false;

        float timeBetweenPatternsMinSeconds = difficultyLevels[currentDifficulty].GetTimeBetweenPatternsMinSeconds();
        float timeBetweenPatternsMaxSeconds = difficultyLevels[currentDifficulty].GetTimeBetweenPatternsMaxSeconds();
        float waitTime = Random.Range(timeBetweenPatternsMinSeconds, timeBetweenPatternsMaxSeconds);
        yield return new WaitForSeconds(waitTime);
    }

    private IEnumerator SpawnEnemy(Enemy enemy, int position, float waitTime)
    {
        int yPosition = position;
        float randomTemp = 0;
        if(position == 2)
        {
            int playerPos = FindObjectOfType<Player>().GetRoundedYPos();
            yPosition = playerPos;
        }
        else if (position == 3)
        {
            int playerPos = FindObjectOfType<Player>().GetRoundedYPos();
            if (Mathf.Abs(playerPos) > 0.5f) { yPosition = 0; }
            else {
                randomTemp = Random.Range(0f, 1f);
                if (randomTemp < 0.5f) { yPosition = -1; }
                else { yPosition = 1; }
                }
        }
        else if (position == 4)
        {
            int playerPos = FindObjectOfType<Player>().GetRoundedYPos();
            randomTemp = Random.Range(0f, 1f);
            if (randomTemp < 0.75f) {
                yPosition = playerPos;
            }
            else {
                if (Mathf.Abs(playerPos) > 0.5f) { yPosition = 0; }
                else
                {
                    randomTemp = Random.Range(0f, 1f);
                    if (randomTemp < 0.5f) { yPosition = -1; }
                    else { yPosition = 1; }
                }
            }
        }

        var newEnemy = Instantiate(
            enemy,
            new Vector2(0f, yPosition),
            Quaternion.identity);


        newEnemy.MoveOrderInLayer(yPosition * -5); // -5 because of selected order meaning

        // Debug.Log(waitTime.ToString());
        yield return new WaitForSeconds(waitTime);
    }
}
