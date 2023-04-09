using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testing {
    public class ItemPickup : MonoBehaviour
    {
        public Item Item;

        void Pickup()
        {
            InventoryManager.Instance.Add(Item);
            Destroy(gameObject);
        }

        private void OnMouseDown()
        {
            Pickup();
            
        }
    }
}