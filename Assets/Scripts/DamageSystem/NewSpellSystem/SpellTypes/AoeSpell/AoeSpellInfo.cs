using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.SpellTypes.Aoe
{
    [CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/Spells/AoeSpell", order = 5)]
    public class AoeSpellInfo : ScriptableObject
    {
        public float castTime;
        public float cooldown;
        public AoeInfo aoeEffect;
    }
}
