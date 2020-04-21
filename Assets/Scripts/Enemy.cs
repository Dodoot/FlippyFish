using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Params
    [SerializeField] int[] possibleYPositions = new int[0];
    [SerializeField] int scoreValue = 1;
    [SerializeField] AudioClip[] soundsArray;
    [SerializeField] float[] soundsVolumes;

    public int[] GetPossibleYPositions() { return possibleYPositions; }

    public void DestroyThis()
    {
        Destroy(gameObject);
        AddScore();
    }

    private void AddScore()
    {
        if (!FindObjectOfType<LevelController>().GetAlreadyLost())
        {
            FindObjectOfType<GameSession>().AddToCurrentScore(scoreValue);
        }
    }

    public void PlaySound(int soundIndex)
    {
        AudioSource.PlayClipAtPoint(
            soundsArray[soundIndex], 
            Camera.main.transform.position, 
            soundsVolumes[soundIndex]);
    }

    public void MoveOrderInLayer(int toMove)
    {
        SpriteRenderer[] childrenSprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer childrenSprite in childrenSprites)
        {
            childrenSprite.sortingOrder += toMove;
        }
    }
}
