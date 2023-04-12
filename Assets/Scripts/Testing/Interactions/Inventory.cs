using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testing {
    public class Inventory : MonoBehaviour
    {
        public List<equipmentItem> items;
        public int maxSlots = 5;
        private int freeSlots;

        public InventoryUI inventoryUi;

        // Start is called before the first frame update
        void Start()
        {
            freeSlots = maxSlots;
        }

        public bool canPickUp()
        {
            return freeSlots > 0;
        }

        public void InsertItem(equipmentItem item)
        {
            if(canPickUp())
            {
                items.Add(item);
                freeSlots--;
                inventoryUi.UpdateUI(item);
            }
        }
    }
}