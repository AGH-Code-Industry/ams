using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Testing.Interactions {
    public class PlayerInteraction : MonoBehaviour {
        public GameObject interactTooltip;
        IInteractable interaction;

        bool canInteract = false;

        private void Start() {
            InputManager.actions.Player.Interact.started += _ => Interact();
        }

        void Interact() {
            if(canInteract) {
                interaction.Use();
                // interactTooltip.SetActive(false); //obiekt nie znika po wyjściu z jego zasięgu
                interaction.Outline(false);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.TryGetComponent(out interaction)) {
                canInteract = true;
                interactTooltip.SetActive(true);
                interaction.Outline(true);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.TryGetComponent(out interaction)) {
                canInteract = false;
                interaction.Outline(false);
                interaction = null;
                // interactTooltip.SetActive(false); //obiekt nie znika po wyjściu z jego zasięgu
            }
        }
    }
}