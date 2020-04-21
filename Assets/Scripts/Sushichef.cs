using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushichef : MonoBehaviour
{
    [SerializeField] AudioClip[] soundsArray;
    [SerializeField] float[] soundsVolumes;

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void PlaySound(int soundIndex)
    {
        AudioSource.PlayClipAtPoint(
            soundsArray[soundIndex],
            Camera.main.transform.position,
            soundsVolumes[soundIndex]);
    }
}
