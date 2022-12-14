using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Spells/PulseFire", order = 2)]
public class PulseFireInfo : ScriptableObject
{
    public float castTime;
    public float cooldown;
    public Rigidbody pellet;
    public ElementalType[] elementals;
}
