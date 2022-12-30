using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ElementalType
{
    public enum Element {FIRE, WATER, PHYS}
    public Element elemental;
    public int damage;
    public int effectVal;
}
