using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class StatsDisplay : MonoBehaviour
{
    public Text NameText;
    public Text ValueText;

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        NameText = texts[0];
        ValueText = texts[1];
    }
}
