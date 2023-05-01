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
            if (count == 1) {
                countText.text = "";
            } else {
                countText.text = count.ToString();
            }
        }
    }

    void Start()
    {
        if (_item) {
            iconImage.enabled = true;
            iconImage.sprite = _item.icon;
        } 
        count = _count;
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
        MoveToDraggingSlot();
        Inventory.HideHoverWindow();
    }

    private void MoveToDraggingSlot() {
        DraggingSlot.instance.transform.position = transform.position;
        DraggingSlot.instance.startDragSlot = transform.parent.GetComponent<InventorySlot>();
        SetSlot(DraggingSlot.instance);
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

    public void OnEndDrag(PointerEventData eventData)     {
        Inventory.ShowHoverWindow(this);
        RaycastResult newSlotRaycastResult = RaycastMouse().FirstOrDefault(raycastResult => raycastResult.gameObject.GetComponent<InventorySlot>());
        if (newSlotRaycastResult.Equals(new RaycastResult())) {
            SetSlot(DraggingSlot.instance.startDragSlot);
        } else {
            InventorySlot newSlot = newSlotRaycastResult.gameObject.GetComponent<InventorySlot>();
            if (newSlot.canAccept(item)) {
                SetSlot(newSlot);
            } else if (newSlot.isTaken && 
                newSlot.typeMatches(item.type) && 
                DraggingSlot.instance.startDragSlot.typeMatches(newSlot.GetComponentInChildren<ItemStack>().item.type)) {
                SwapWithDragged(newSlot);
            } else {
                SetSlot(DraggingSlot.instance.startDragSlot);
            }
        }
    }

    private void SwapWithDragged(InventorySlot newSlot) {
        ItemStack swapStack = newSlot.GetComponentInChildren<ItemStack>();
        SetSlot(newSlot);
        swapStack.SetSlot(DraggingSlot.instance.startDragSlot);
    }

    public void SetSlot(Slot slot) {
        transform.SetParent(slot.transform);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;   
    }
}