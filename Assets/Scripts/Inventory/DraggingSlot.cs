using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingSlot : MonoBehaviour
{
    static public DraggingSlot instance;
    static public Transform startDragSlot;
    public RectTransform rectTransform;

    private void Awake() {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
    }
}
