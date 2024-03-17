using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public TextMeshProUGUI infoText;
    public void Awake()
    {
        instance = this; 
    }

    public void UpdateInfoText(string text)
    {
        infoText.text = text;
    }
}
