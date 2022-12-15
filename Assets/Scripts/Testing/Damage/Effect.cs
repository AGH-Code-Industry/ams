using System.Collections.Generic;
using UnityEngine;

public enum EffectType {
    Steam,
    Flame,
    Mud,
    Wet,
    Burning,
}

[System.Serializable]
public struct EffectReaction {
    public Effect factor;
    public Effect result;
}

[CreateAssetMenu(fileName = "new Effect", menuName = "ScriptableObjects/Effect")]
public class Effect : ScriptableObject {
    public EffectType effectType;
    public EffectReaction[] effectReactions;
}