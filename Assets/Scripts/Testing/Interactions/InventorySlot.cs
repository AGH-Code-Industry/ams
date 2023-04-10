using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace testing {
    public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Image icon;

        equipmentItem item;

        public void AddItem(equipmentItem newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
        }
        public bool IsFree()
        {
            return item == null;
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            if (item)
            {
                string message = item.name + "\n" + "Type: " + item.type + "\n" + item.description + "\n" + "Primary stat: " + item.primaryStat;
                ToolTipManager._instance.SetAndShowToolTip(message);
            }
        }
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            if(item)
            {
                ToolTipManager._instance.HideToolTip();
            }
        }
    }
}