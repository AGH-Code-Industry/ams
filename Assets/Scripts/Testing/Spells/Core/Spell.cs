using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject {
    public float _manaCost;
    public float _cooldown;
}
