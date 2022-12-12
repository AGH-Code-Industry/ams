using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    public equipmentItem Collect();

    void Outline(bool value);
}
