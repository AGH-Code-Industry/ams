using DamageSystem.ReceiveDamage.Elementals.Elementals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.SpellTypes.Projectile {

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Spells/SimpleProjectile", order = 3)]
    public class SimpleProjectileInfo : ScriptableObject {
        public float castTime;
        public float cooldown;
        public float force;
        public int projectileLifeSpan;
        public Rigidbody projectile;
        public List<AttackElemental> elementals;
    }
}
