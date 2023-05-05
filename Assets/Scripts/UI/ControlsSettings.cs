using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlsSettings : MonoBehaviour
{
    public GameObject rebindingOverlay;
    private PlayerInputActions playerInputActions;

    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.UI.Enable();
        rebindingOverlay.SetActive(false);

        playerInputActions.UI.Cancel.performed += ctx => rebindingOverlay.SetActive(false);
    }

    void Update()
    {
      
    }

    public void StartRebinding() {
        rebindingOverlay.SetActive(true);
    }

    public void RebindComplete() {
        rebindingOverlay.SetActive(false);
    }

    public void Back() {
        SceneManager.LoadScene("SettingsMenu");
    }

}
