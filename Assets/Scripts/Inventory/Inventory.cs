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
    public void AddItem(Item item)
    {
        List<ItemStack> foundStacks = GetStacksWithFreeSpace(item);
        if (foundStacks.Count != 0)
        {
            foundStacks[0].count++;
        } 
        else 
        {
            ItemStack newStack = Instantiate(itemStackPrefab);
            newStack.item = item;
            newStack.transform.SetParent(itemStacksContainer.transform);
        }
    }

    private List<ItemStack> GetStacksWithFreeSpace(Item item, int count = 1)
    {
        List<ItemStack> itemStacks = new List<ItemStack>(instance.itemStacksContainer.GetComponentsInChildren<ItemStack>());
        return itemStacks.FindAll(s => s.item.name == item.name && s.CanAdd(count));
    }

    private static Inventory instance;

    // private int width = 9;
    // private int height = 4;

    void Awake() {
        instance = this;
    }

    public static void ShowHoverWindow(ItemStack stack)
    {
        instance.hoverWindow.gameObject.SetActive(true);
        instance.hoverWindow.stack = stack;
    }

    public static void HideHoverWindow()
    {
        instance.hoverWindow.gameObject.SetActive(false);
    }
}