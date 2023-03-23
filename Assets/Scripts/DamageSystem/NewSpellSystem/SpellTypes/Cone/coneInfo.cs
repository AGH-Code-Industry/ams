using DamageSystem.ReceiveDamage.Elementals.Elementals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace DamageSystem.NewSpellSystem.SpellTypes.Cone
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Spells/Cone", order = 4)]
    public class coneInfo : ScriptableObject
    {
       // public float cooldown;
       // public float castTime;
        public float tickRate = 0.5f;
        //public float rangeY = 5.85f;
        //public float spreadHorizontalX = 3.75f;
        //public float spreadVerticalZ = 1.25f;
        //[System.NonSerialized]
        //public float rotationX = -90f;
        public VisualEffectAsset particles;
        public List<AttackElemental> elementals;
        public bool isPrimary = true;
        public bool isSecondary = false;
    }
}
