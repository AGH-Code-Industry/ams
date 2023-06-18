using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyRebind : MonoBehaviour
{
    private Color initialColor;
    private TextMeshProUGUI buttonText;
    void Start()
    {
        buttonText = this.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>();
        initialColor = buttonText.color;
    }

    public void SetError() {
        buttonText.color = Color.red;
    }

    public void ResetTextColor() {
        buttonText.color = initialColor;
    }
}
