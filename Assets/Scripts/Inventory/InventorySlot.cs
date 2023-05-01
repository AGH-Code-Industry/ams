using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public ItemType type = ItemType.Normal;
    private bool isTaken { 
        get => GetComponentInChildren<ItemStack>() != null;
    } 

    private bool canAcceptType(ItemType type) {
        if (this.type == ItemType.Normal) return true;
        return this.type == type;
    }

    public bool canAccept(Item item) {
        return !isTaken && canAcceptType(item.type);
    }
}
