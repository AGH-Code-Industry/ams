using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace team4.equipment {

    public class ItemStack : MonoBehaviour
    {

        [SerializeField] private Image iconImage;
        [SerializeField] private Text countText;
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
    }

}
