using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemStack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Item _item;
    [SerializeField] private int _count = 1;

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

    void Start()
    {
        if (_item) {
            iconImage.enabled = true;
            iconImage.sprite = _item.icon;
        } 
        countText.text = count.ToString();
    }

    public bool CanAdd(int amount = 1) {
        return count + amount <= item.stackSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Inventory.ShowHoverWindow(this);
    }       
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.HideHoverWindow();
    }
}