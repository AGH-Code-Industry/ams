using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data", menuName ="ScriptableObjects/Equipment", order =2)]
public class equipmentItem : ScriptableObject
{
    public enum itemType {MELEE, ARMOR, WAND}

    public new string name;
    public Sprite itemIcon;
    public string description;
    public itemType type;
    public int primaryStat;
}
