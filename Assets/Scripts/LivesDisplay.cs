using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesDisplay : MonoBehaviour
{
    TextMeshProUGUI livesText;
    Player player;

    void Start()
    {
        livesText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        livesText.text = player.GetNumberOfLives().ToString();
    }
}
