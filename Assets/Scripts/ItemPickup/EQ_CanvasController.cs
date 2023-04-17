using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQ_CanvasController : MonoBehaviour
{
    public GameObject MessagePanel;
    public void OpenMessagePanel()
    {
        MessagePanel.SetActive(true);
    }

    public void CloseMessagePanel() 
    {
        MessagePanel.SetActive(false);
    }
    
}
