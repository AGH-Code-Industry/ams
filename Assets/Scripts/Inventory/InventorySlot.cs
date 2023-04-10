using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public bool isTaken { 
        get => GetComponentInChildren<ItemStack>() != null;
    } 
}
