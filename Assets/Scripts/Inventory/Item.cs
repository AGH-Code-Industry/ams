using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string description;
    public Sprite icon;
    public int stackSize = 64;
    public ItemType type = ItemType.Normal;
}