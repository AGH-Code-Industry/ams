using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static public PlayerInputActions actions;

    private void Awake() {
        actions = new PlayerInputActions();
        actions.Enable();
    }
}
