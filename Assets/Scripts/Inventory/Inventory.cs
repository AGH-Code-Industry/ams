using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private HoverWindow hoverWindow;
    [SerializeField] private ItemStack itemStackPrefab;
    [SerializeField] private GameObject slotsContainer;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Item addItemTestItem;
    private List<InventorySlot> slots;
    private static Inventory instance;

    private void Start() {
        instance = this;
        slots = new List<InventorySlot>(slotsContainer.GetComponentsInChildren<InventorySlot>());
        InputManager.actions.Player.Inventory.started += _ => Toggle();
        InputManager.actions.Player.AddInventoryItemTest.started += _ => AddItemTest();
    }

    private void Toggle() {
        canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
    }

    private void AddItemTest() {
        AddItem(addItemTestItem);
    }

    public void AddItem(Item item)
    {
        List<ItemStack> foundStacks = GetStacksWithFreeSpace(item);
        if (foundStacks.Count != 0)
        {
            foundStacks[0].count++;
        } 
        else 
        {
            InventorySlot slot = GetFreeSlot(item);
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

    private InventorySlot GetFreeSlot(Item item)
    {
        return GetSlots().FirstOrDefault(slot => slot.canAccept(item));
    }

    public static void ShowHoverWindow(ItemStack stack)
    {
        instance.hoverWindow.Show(stack);
    }

    public static void HideHoverWindow()
    {
        instance.hoverWindow.Hide();
    }
}