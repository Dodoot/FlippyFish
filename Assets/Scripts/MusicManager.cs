using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Start()
    {
        SetupAudioListener();
    }

    public void TriggeredUpdate()
    {
        SetupAudioListener();
    }

    private void SetupAudioListener()
    {
        bool isSoundOn = PlayerPrefsController.GetSoundOn() != 0;
        // Camera.main.gameObject.GetComponent<AudioListener>().enabled = isSoundOn;
        AudioListener.pause = !isSoundOn;
    }
}
