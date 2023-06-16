using DamageSystem.NewSpellSystem.SpellTypes.Projectile;
using UnityEngine;

namespace Enemies.Models.AttackModels {
    public class ChairAttack : AttackModel{
        
        [SerializeField] [Tooltip("Spell to attack")]
        private SimpleProjectile spell;

        private float _nextAvailableCastTime; 
        public override bool Attack() {
            if (!IsAbleToCast()) {
                return false;
            }
            transform.LookAt(player);
            spell.Cast(transform);
            _nextAvailableCastTime = Time.time + spell.GetCastTime();
            return true;
        }

        private bool IsAbleToCast() {
            return Time.time >= _nextAvailableCastTime;
        }
    }
}