using DamageSystem.ReceiveDamage.Elementals.Elementals;
using System.Collections;
using System.Collections.Generic;
using DamageSystem.NewSpellSystem.SpellTypes.Aoe;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.SpellTypes.Projectile {

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Spells/SimpleProjectile", order = 3)]
    public class SimpleProjectileInfo : ScriptableObject {
        public string spellName = "default";
        public string description = "default";
        public float castTime;
        public float cooldown;
        public float force;
        public int projectileLifeSpan;
        public Rigidbody projectile;
        public List<AttackElemental> elementals;
        public AoeInfo aoe;
        public bool isPrimary;
        public bool isSecondary;
    }
}
