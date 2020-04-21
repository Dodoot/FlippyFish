using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceLauncher : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<LevelLoader>().LoadGame();
        }
    }
}
