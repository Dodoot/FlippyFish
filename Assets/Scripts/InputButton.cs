using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] string inputMessage = "No Input";

    Player myPlayer;

    private void Start()
    {
        myPlayer = FindObjectOfType<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        myPlayer.SetCurrentInput(inputMessage);
    }
}
