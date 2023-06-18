using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Samples.RebindUI;
using TMPro;

public class ControlsSettings : MonoBehaviour
{
    public Button backButton;
    public Color errorColor;

    private RebindSaveLoad rebindSaveLoad;

    void Start() {
        rebindSaveLoad = FindObjectOfType<RebindSaveLoad>();
        this.CheckBindingsValidity();
    }

    public void Back() {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void ResetSettings() {
        var rebindComponents = FindObjectsOfType<RebindActionUI>();
        foreach (var component in rebindComponents) {
            component.ResetToDefault();
        }

        ResetBindingsTextColor();
        backButton.interactable = true;
    }

    // Sprawdza czy wszystkie przyciski mają unikalne przypisania
    public void CheckBindingsValidity() {
        var bindings = new Dictionary<string, KeyRebind>();
        var rebindComponents = FindObjectsOfType<RebindActionUI>();
        bool areBindingsValid = true;
        foreach (var component in rebindComponents) {
            string binding = component.GetBindingString();
            KeyRebind keyRebind = component.GetComponent<KeyRebind>();
            keyRebind.ResetTextColor();

            if (bindings.ContainsKey(binding)) {
                areBindingsValid = false;
                bindings[binding].SetError();
                keyRebind.SetError();
            } else {
                bindings.Add(binding, keyRebind);
            }
        }

        if (!areBindingsValid) {
            rebindSaveLoad.areControlsValid = false;
            backButton.interactable = false;
        } else {
            rebindSaveLoad.areControlsValid = true;
            backButton.interactable = true;
        }
    }

    private void ResetBindingsTextColor() {
        var keyRebinds = FindObjectsOfType<KeyRebind>();
        foreach (var keyRebind in keyRebinds) {
            keyRebind.ResetTextColor();
        }
    }
}