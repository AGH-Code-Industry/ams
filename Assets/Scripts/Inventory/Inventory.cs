using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemStack> itemStacks;

    public void Add(Item item)
    {
        ItemStack foundStack = itemStacks.FirstOrDefault(s => s.item.name == item.name);
        if (foundStack != null)
        {
            foundStack.count++;
        } 
        else 
        {
            itemStacks.Append(new ItemStack(item));
        }
    }
}
