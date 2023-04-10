using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryShortcut : MonoBehaviour
{
    [SerializeField] private KeyCode openKey = KeyCode.I;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Item item;

    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            inventory.AddItem(item);
        }
    }
}