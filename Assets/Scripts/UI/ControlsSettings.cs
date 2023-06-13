using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Samples.RebindUI;

public class ControlsSettings : MonoBehaviour
{
    public void Back() {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void ResetSettings() {
        var rebindComponents = FindObjectsOfType<RebindActionUI>();
        foreach (var component in rebindComponents) {
            component.ResetToDefault();
        }
    }
}