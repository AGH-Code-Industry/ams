using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class ItemStack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Item _item;
    [SerializeField] private int _count = 1;
    private RectTransform rectTransform;

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
        rectTransform = GetComponent<RectTransform>();
    }

    public bool CanAdd(int amount = 1) {
        return count + amount <= item.stackSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Inventory.ShowHoverWindow(this);
        Cursor.SetCursor(CursorTextures.instance.cursorPointer, Vector2.zero, CursorMode.Auto);
    }       
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.HideHoverWindow();
        Cursor.SetCursor(CursorTextures.instance.cursorDefault, Vector2.zero, CursorMode.Auto);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DraggingSlot.instance.transform.position = transform.position;
        DraggingSlot.startDragSlot = transform.parent;
        transform.SetParent(DraggingSlot.instance.transform);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;   
        Inventory.HideHoverWindow();
    }

    public void OnDrag(PointerEventData eventData)
    {
        DraggingSlot.instance.rectTransform.anchoredPosition += eventData.delta;
    }

    public List<RaycastResult> RaycastMouse(){
        PointerEventData pointerData = new PointerEventData (EventSystem.current)
        {
            pointerId = -1,
        };
        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        return results;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Inventory.ShowHoverWindow(this);
        RaycastResult defaultRaycastResult = new RaycastResult();
        RaycastResult newSlotRaycastResult = RaycastMouse().FirstOrDefault(raycastResult => raycastResult.gameObject.GetComponent<InventorySlot>());
        if (!newSlotRaycastResult.Equals(defaultRaycastResult) && !newSlotRaycastResult.gameObject.GetComponent<InventorySlot>().isTaken) {
            transform.SetParent(newSlotRaycastResult.gameObject.transform);
        } else {
            transform.SetParent(DraggingSlot.startDragSlot);
        }
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;   
    }
}