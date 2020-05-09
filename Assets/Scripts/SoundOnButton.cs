using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundOnButton : MonoBehaviour, IPointerDownHandler
{
    Transform onImage;
    Transform offImage;

    private void Start()
    {
        onImage = transform.Find("OnImage");
        offImage = transform.Find("OffImage");
        ChooseImage();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerPrefsController.SwitchSoundOn();
        FindObjectOfType<MusicManager>().TriggeredUpdate();
        ChooseImage();
    }

    private void ChooseImage()
    {
        if(PlayerPrefsController.GetSoundOn() == 0)
        {
            onImage.gameObject.SetActive(false);
            offImage.gameObject.SetActive(true);
        }
        else
        {
            onImage.gameObject.SetActive(true);
            offImage.gameObject.SetActive(false);
        }
    }
}
