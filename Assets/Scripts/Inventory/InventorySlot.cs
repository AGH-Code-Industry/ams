using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : Slot
{
    public ItemType type = ItemType.Normal;
    public bool isTaken { 
        get => GetComponentInChildren<ItemStack>() != null;
    } 

    public bool typeMatches(ItemType type) {
        if (this.type == ItemType.Normal) return true;
        return this.type == type;
    }

    public bool canAccept(Item item) {
        return !isTaken && typeMatches(item.type);
    }
}
