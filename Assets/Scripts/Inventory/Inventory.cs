using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private HoverWindow hoverWindow;
    [SerializeField] private GameObject itemStacksContainer;
    [SerializeField] private ItemStack itemStackPrefab;
    [SerializeField] private GameObject slotsContainer;
    private List<InventorySlot> slots;

    public void AddItem(Item item)
    {
        List<ItemStack> foundStacks = GetStacksWithFreeSpace(item);
        if (foundStacks.Count != 0)
        {
            foundStacks[0].count++;
        } 
        else 
        {
            InventorySlot slot = GetFreeSlot();
            if (slot) {
                ItemStack newStack = Instantiate(itemStackPrefab);
                newStack.item = item;
                newStack.transform.SetParent(slot.transform, false);
            } else {
                Debug.Log("Przedmiot nie zmieścił się w inventory");
            }
        }
    }

    private List<ItemStack> GetStacksWithFreeSpace(Item item, int count = 1)
    {
        List<ItemStack> itemStacks = new List<ItemStack>(instance.slotsContainer.GetComponentsInChildren<ItemStack>());
        return itemStacks.FindAll(s => s.item.name == item.name && s.CanAdd(count));
    }

    private List<InventorySlot> GetSlots()
    {
        return new List<InventorySlot>(slotsContainer.GetComponentsInChildren<InventorySlot>());
    }

    private InventorySlot GetFreeSlot()
    {
        return GetSlots().FirstOrDefault(slot => !slot.isTaken);
    }

    private static Inventory instance;

    void Awake() {
        instance = this;
        slots = new List<InventorySlot>(slotsContainer.GetComponentsInChildren<InventorySlot>());
    }

    public static void ShowHoverWindow(ItemStack stack)
    {
        instance.hoverWindow.Show(stack);
        // instance.hoverWindow.gameObject.SetActive(true);
        // instance.hoverWindow.stack = stack;
    }

    public static void HideHoverWindow()
    {
        instance.hoverWindow.Hide();
        // instance.hoverWindow.gameObject.SetActive(false);
    }
}