using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingSlot : Slot
{
    static public DraggingSlot instance;
    public InventorySlot startDragSlot;
    public RectTransform rectTransform;

    private void Awake() {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
    }
}
