using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrlOpener : MonoBehaviour
{
    [SerializeField] string url = "";

    public void OpenUrl()
    {
        Application.OpenURL(url);
    }
}
