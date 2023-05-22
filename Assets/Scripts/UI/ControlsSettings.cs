using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlsSettings : MonoBehaviour
{
    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;
    public Button attack1Button;
    public Button attack2Button;
    public GameObject rebindingOverlay;
    public InputActionAsset actions;
    private PlayerInputActions playerInputActions;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    void Awake() {
        upButton.onClick.AddListener(() => StartRebinding("Move", "WASD", "Up"));
        downButton.onClick.AddListener(() => StartRebinding("Move", "WASD", "Down"));
        leftButton.onClick.AddListener(() => StartRebinding("Move", "WASD", "Left"));
        rightButton.onClick.AddListener(() => StartRebinding("Move", "WASD", "Right"));
        attack1Button.onClick.AddListener(() => StartRebinding("PrimarySpell1", "Left Button"));
        attack2Button.onClick.AddListener(() => StartRebinding("PrimarySpell2", "Right Button"));

        playerInputActions = new PlayerInputActions();
        // playerInputActions.UI.Cancel.performed += ctx => RebindComplete();

        playerInputActions.UI.Enable();
        rebindingOverlay.SetActive(false);
    }

    void OnDestroy() {
        // playerInputActions.UI.Cancel.performed -= ctx => RebindComplete();

        playerInputActions.UI.Disable();
    }

    public void StartRebinding(string actionName, string bindingName, string partName = "") {
        // NOTE: Rebinding function correctly changes effective path of the binding, but the changes are not reflected in th game
        // TODO: Find the reason why this doesn't work as expected
        rebindingOverlay.SetActive(true);

        int bindingIndex;

        InputAction action = actions.FindActionMap("Player").FindAction(actionName);

        // If partName is provided, we assume it's a composite binding
        if (partName != "") {
             bindingIndex = action.ChangeCompositeBinding(bindingName).NextPartBinding(partName).bindingIndex;
        } else {
            bindingIndex = action.ChangeBinding(bindingName).bindingIndex;
        }

        rebindingOperation = action.PerformInteractiveRebinding().WithTargetBinding(bindingIndex).OnComplete(_ => RebindComplete()).OnCancel(_ => RebindComplete()).Start();
    }

    public void RebindComplete() {
        rebindingOperation.Dispose();
        rebindingOverlay.SetActive(false);
    }

    public void Back() {
        SceneManager.LoadScene("SettingsMenu");
    }

}
