using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    Slider mySlider;
    int lastValue = 1;
    int mySliderValue = 1;

    Transform myEasyText;
    Transform myNormalText;
    Transform myHardText;

    void Start()
    {
        mySlider = transform.Find("Slider").GetComponent<Slider>();
        myEasyText = transform.Find("Easy Text");
        myNormalText = transform.Find("Normal Text");
        myHardText = transform.Find("Hard Text");

        mySlider.value = PlayerPrefsController.GetDifficulty();
        mySliderValue = Mathf.RoundToInt(mySlider.value);
        lastValue = mySliderValue;

        SetCorrectText();
    }
    
    void Update()
    {
        mySliderValue = Mathf.RoundToInt(mySlider.value);
        if (lastValue != mySliderValue)
        {
            PlayerPrefsController.SetDifficulty(mySliderValue);
            lastValue = mySliderValue;

            SetCorrectText();
        }
    }

    private void SetCorrectText()
    {
        if(mySliderValue == 0)
        {
            myEasyText.gameObject.SetActive(true);
            myNormalText.gameObject.SetActive(false);
            myHardText.gameObject.SetActive(false);
        }
        else if (mySliderValue == 1)
        {
            myEasyText.gameObject.SetActive(false);
            myNormalText.gameObject.SetActive(true);
            myHardText.gameObject.SetActive(false);
        }
        else if (mySliderValue == 2)
        {
            myEasyText.gameObject.SetActive(false);
            myNormalText.gameObject.SetActive(false);
            myHardText.gameObject.SetActive(true);
        }
        else { Debug.LogError("WTF is this slider value"); }
    }
}
