using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControlsSettings : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public GameObject rebindingOverlay;

    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.UI.Enable();
        playerInputActions.Player.Disable();
        rebindingOverlay.SetActive(false);
        playerInputActions.UI.Cancel.performed += ctx => rebindingOverlay.SetActive(false);
    }

    void Update()
    {
      
    }

    public void StartRebinding() {
        rebindingOverlay.SetActive(true);
    }

}
