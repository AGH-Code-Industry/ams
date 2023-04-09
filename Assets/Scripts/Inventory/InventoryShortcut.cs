using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryShortcut : MonoBehaviour
{
    [SerializeField] private KeyCode openKey = KeyCode.I;
    [SerializeField] private InventoryUI inventoryUI;

    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeInHierarchy);
        }
    }
}