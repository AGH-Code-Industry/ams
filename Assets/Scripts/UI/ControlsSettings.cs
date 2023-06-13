using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlsSettings : MonoBehaviour
{
    public void Back() {
        SceneManager.LoadScene("SettingsMenu");
    }
}