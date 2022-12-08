using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/PickUp", order =1)]
public class pickUp : ScriptableObject
{
    public new string name;
    public int healValue;
    public int manaValue;
}
