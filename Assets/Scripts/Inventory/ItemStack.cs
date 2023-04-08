using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
namespace team4.equipment {

    public class ItemStack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text countText;
        [SerializeField] private Item _item;
        [SerializeField] private int _count = 0;

        public Item item 
        {
            get => _item;
            set {
                _item = value;
                iconImage.sprite = _item.icon;
            }
        }

        public int count 
        {
            get => _count;
            set {
                _count = value;
                countText.text = count.ToString();
            }
        }

        public ItemStack(Item item) 
        {
            _item = item;
        }

        void Start()
        {
            if (_item) {
                iconImage.enabled = true;
                iconImage.sprite = _item.icon;
            } 
            countText.text = count.ToString();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            InventoryUI.ShowHoverWindow(this);
        }       
        
        public void OnPointerExit(PointerEventData eventData)
        {
            InventoryUI.HideHoverWindow();
        }
    }

}
