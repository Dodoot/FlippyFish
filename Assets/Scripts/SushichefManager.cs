using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushichefManager : MonoBehaviour
{
    [SerializeField] Sushichef[] SushichefPrefabs;
    [SerializeField] float pauseTimeSeconds = 1f;

    bool readyForTrigger = false;
    string storedKind = "Happy";

    int[] START_INDEXS = { 0 };
    int[] FASTER_INDEXS = { 1 };
    int[] FIRE_INDEXS = { 2 };
    int[] CUT_INDEXS = { 3 };
    int[] COMBO_INDEXS = { 4 };

    public void SetReadyForTrigger(bool newReadyForTrigger) { readyForTrigger = newReadyForTrigger; }

    public void TriggerSushichef(string sushichefKind)
    {
        // TODO select at random
        int sushichefIndex = 0;
        if (sushichefKind == "Start") { sushichefIndex = START_INDEXS[0]; }
        else if (sushichefKind == "Faster") { sushichefIndex = FASTER_INDEXS[0]; }
        else if (sushichefKind == "Fire") { sushichefIndex = FIRE_INDEXS[0]; }
        else if (sushichefKind == "Cut") { sushichefIndex = CUT_INDEXS[0]; }
        else if (sushichefKind == "Combo") { sushichefIndex = COMBO_INDEXS[0]; }

        Instantiate(
            SushichefPrefabs[sushichefIndex],
            Vector2.zero,
            Quaternion.identity);
    }

    public void TriggerSushichefWithPause(string sushichefKind)
    {
        FindObjectOfType<EnemiesManager>().TriggerEmptyLoop(pauseTimeSeconds);
        storedKind = sushichefKind;
    }

    private void Start()
    {
        TriggerSushichef("Start");
    }

    private void Update()
    {
        if (readyForTrigger)
        {
            TriggerSushichef(storedKind);
            readyForTrigger = false;
        }
    }
}
