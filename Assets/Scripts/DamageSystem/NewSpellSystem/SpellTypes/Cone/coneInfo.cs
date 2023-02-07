using DamageSystem.ReceiveDamage.Elementals.Elementals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.SpellTypes.Cone
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Spells/Cone", order = 4]
    public class coneInfo : ScriptableObject
    {
        public float cooldown;
        public float castTime;
        public float range;
        public float spread;
        public ParticleSystem particles;
        public List<AttackElemental> elementals;
    }
}
