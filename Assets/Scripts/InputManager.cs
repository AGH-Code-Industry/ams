using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    static public PlayerInputActions actions;
    static public List<InputAction> primaryCastActions;
    static public List<InputAction> secondaryCastActions;

    private void Awake() {
        // Zapobiega istnieniu dwóch menadżerów na raz
        if (FindObjectsOfType<InputManager>().Length > 1) {
            Destroy(this.gameObject);
            return;
        }

        actions = new PlayerInputActions();
        RebindSaveLoad.LoadRebinds();
        actions.Enable();

        primaryCastActions = new List<InputAction>{ 
            actions.Player.PrimarySpell1,
            actions.Player.PrimarySpell2,
            actions.Player.PrimarySpell3,
        };

        secondaryCastActions = new List<InputAction>{ 
            actions.Player.SecondarySpell1,
            actions.Player.SecondarySpell2,
            actions.Player.SecondarySpell3,
        };

        DontDestroyOnLoad(this.gameObject);
    }
}
