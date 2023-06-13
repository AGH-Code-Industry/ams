using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Samples.RebindUI;

public class ControlsSettings : MonoBehaviour
{
    public Button backButton;

    public void Back() {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void ResetSettings() {
        var rebindComponents = FindObjectsOfType<RebindActionUI>();
        foreach (var component in rebindComponents) {
            component.ResetToDefault();
        }
        backButton.interactable = true;
    }

    // Sprawdza czy wszystkie przyciski mają unikalne przypisania
    public void CheckBindingsValidity() {
        var bindings = new List<string>();
        var rebindComponents = FindObjectsOfType<RebindActionUI>();
        foreach (var component in rebindComponents) {
            string binding = component.GetBindingString();
            if (bindings.Contains(binding)) {
                backButton.interactable = false;
                return;
            } else {
                bindings.Add(binding);
            }
        }

        backButton.interactable = true;
    }
}