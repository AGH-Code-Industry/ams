using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.SpellTypes.Aoe
{
    [CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/Spells/AoeSpell", order = 5)]
    public class AoeSpellInfo : ScriptableObject
    {
        public string spellName = "default";
        public string description = "default";
        public float castTime;
        public float cooldown;
        public AoeInfo aoeEffect;
        public bool isPrimary;
        public bool isSecondary;
    }
}
