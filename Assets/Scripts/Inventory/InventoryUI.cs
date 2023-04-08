using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace team4.equipment {

    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private HoverWindow hoverWindow;

        private static InventoryUI instance;

        private int width = 9;
        private int height = 4;

        void Awake() {
            instance = this;
        }

        public static void ShowHoverWindow(ItemStack stack)
        {
            instance.hoverWindow.gameObject.SetActive(true);
            instance.hoverWindow.SetItemStack(stack);
        }

        public static void HideHoverWindow()
        {
            instance.hoverWindow.gameObject.SetActive(false);
        }
    }


}
