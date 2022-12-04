using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing.CreativeMenu {
    public class MenuController : MonoBehaviour {
        [Header("GameObjects")]
        [SerializeField] private GameObject menu;
        
        private bool _isMenuActive;
        private void Start() {
            ToggleMenu(false);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.F1)) {
                ToggleMenu();
            }
        }

        /// <summary>
        /// Toggles menu on and off. When provided with argument, can force activation or deactivation.
        /// </summary>
        /// <param name="force">Force activation or deactivation.</param>
        private void ToggleMenu(bool? force = null) {
            _isMenuActive = force ?? !_isMenuActive;
            
            if (_isMenuActive) ActivateMenu();
            else DeactivateMenu();
        }

        private void ActivateMenu() {
            menu.SetActive(true);
        }

        private void DeactivateMenu() {
            menu.SetActive(false);
        }

        private void LoadResources() {
            
        }
    }
}
