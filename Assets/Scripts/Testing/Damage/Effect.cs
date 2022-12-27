using System.Collections.Generic;
using UnityEngine;

namespace Testing.Damage
{
    public enum EffectType
    {
        Steam,
        Flame,
        Mud,
        Wet,
        Burning,
    }

    [System.Serializable]
    public struct EffectReaction
    {
        public Effect factor;
        public Effect result;
    }

    [CreateAssetMenu(fileName = "new Effect", menuName = "ScriptableObjects/Effect")]
    public class Effect : ScriptableObject
    {
        public EffectType effectType;
        public List<EffectReaction> effectReactions;
        public float duration;
    }

    public class ExtendedEffect
    {
        public Effect baseInfo;
        public float startTime;
        public float endTime;
        
        public ExtendedEffect(Effect baseInfo)
        {
            this.baseInfo = baseInfo;
            startTime = Time.time;
            endTime = startTime + baseInfo.duration;
        }
    }
}