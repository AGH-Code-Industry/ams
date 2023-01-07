using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Spell : ScriptableObjectWithId {
    public float _manaCost;
    public float _cooldown;
}
