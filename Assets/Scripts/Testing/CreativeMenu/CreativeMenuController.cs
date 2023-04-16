using System;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Testing.CreativeMenu {
    public class CreativeMenuController : MonoBehaviour {
        [Header("GameObjects")]
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject rowPrefab;

        [Header("Settings")]
        [SerializeField] private short columnsCount;

        [Header("Resources paths")]
        [SerializeField] private string itemsPath;

        private bool _isMenuActive;
        private List<List<ItemSO>> _itemsMatrix;
        private void Start() {
            ToggleMenu(false);
            CreateItemMatrix();
            PopulateMenu();
            InputManager.actions.Player.CreativeMenuToggle.started += _ => ToggleMenu();
        }

        /// <summary>
        /// Toggles menu on and off. When provided with argument, can force activation or deactivation.
        /// </summary>
        /// <param name="force">Force activation or deactivation.</param>
        private void ToggleMenu(bool? force = null) {
            _isMenuActive = force ?? !_isMenuActive;

            if(_isMenuActive) ActivateMenu();
            else DeactivateMenu();
        }

        private void ActivateMenu() {
            menu.SetActive(true);
        }

        private void DeactivateMenu() {
            menu.SetActive(false);
        }

        private ItemSO[] LoadResources() {
            var items = Resources.LoadAll<ItemSO>(itemsPath);

            if(items.Length == 0) {
                throw new ResourcesNotFoundException("Resources for Creative Menu could not be found.");
            }

            return items;
        }

        private void CreateItemMatrix() {
            ItemSO[] items = LoadResources();
            _itemsMatrix = new List<List<ItemSO>>();

            short currentItemIndex = 0;
            var rows = Math.Ceiling((double)items.Length / columnsCount);

            for(var i = 0; i < rows; i++) {
                List<ItemSO> itemsInRow = new List<ItemSO>();
                for(var j = 0; j < columnsCount && currentItemIndex < items.Length; j++) {
                    itemsInRow.Add(items[currentItemIndex++]);
                }
                _itemsMatrix.Add(itemsInRow);
            }
        }

        private void PopulateMenu() {
            foreach(var row in _itemsMatrix) {
                var rowObject = Instantiate(rowPrefab, content.transform);
                var buttons = rowObject.GetComponentsInChildren<Button>();

                int buttonIndex = 0;

                foreach(var item in row) {
                    Button button = buttons[buttonIndex++];
                    button.GetComponentInChildren<TextMeshProUGUI>().SetText(item.itemName);
                    button.onClick.AddListener(() => { SpawnItem(item); });
                }
            }
        }

        private void SpawnItem(ItemSO item) {
            Debug.Log("Spawning item " + item.itemName);
        }
    }
}
