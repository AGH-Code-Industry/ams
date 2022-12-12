using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Outline))]
public class basicEquipment : MonoBehaviour, IEquipment
{
    public equipmentItem itemData;
    Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
    }

    public equipmentItem Collect()
    {
        Destroy(gameObject);
        return itemData;
    }

    public void Outline(bool value)
    {
        outline.enabled = value;
    }
}
