using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryShortcut : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Item item;

    private void Awake() {
        InputManager.actions.Player.Inventory.started += _ => ToggleInventory();
        InputManager.actions.Player.AddInventoryItemTest.started += _ => AddInventoryItemTest();
    }

    private void ToggleInventory() {
        inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);
    }

    private void AddInventoryItemTest() {
        inventory.AddItem(item);
    }
}