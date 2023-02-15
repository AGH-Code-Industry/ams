using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
using UnityEngine.VFX;

namespace DamageSystem.NewSpellSystem.SpellTypes.Aoe
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effects/Aoe", order = 1)]
    public class AoeInfo : ScriptableObject
    {
        public float aoeRange;
        public List<AttackElemental> elementals;
        public VisualEffectAsset vfx;
    }
}
