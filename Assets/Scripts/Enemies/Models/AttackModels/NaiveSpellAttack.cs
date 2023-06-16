using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Enemies.Models.AttackModels {
    public class NaiveSpellAttack : AttackModel {
        [SerializeField] [Tooltip("Spell to attack")]
        private DamageSystem.NewSpellSystem.Core.Spell spell;

        private float _nextAvailableCastTime;
        
        public override void StartModel(){}
        public override bool Attack() {
            transform.LookAt(player);
            if (!IsAbleToCast()) {
                return false;
            }
            
            CastSpell();
            return true;
        }

        private void CastSpell() {
            spell.Cast(transform);
            _nextAvailableCastTime = Time.time + spell.GetCooldown();
        }

        private bool IsAbleToCast() {
            return Time.time >= _nextAvailableCastTime;
        }
    }
}
