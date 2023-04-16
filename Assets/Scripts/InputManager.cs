using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static public PlayerInputActions actions;

    private void Start() {
        actions = new PlayerInputActions();
        actions.Enable();
    }
}
