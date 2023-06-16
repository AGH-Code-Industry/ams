using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DamageSystem.NewSpellSystem.Core;

namespace Enemies.Models.AttackModels {
    public class NaiveSpellAttack : AttackModel {
        [SerializeField] [Tooltip("Spell to attack")]
        private DamageSystem.NewSpellSystem.Core.Spell spell;

        private float _nextAvailableCastTime; 
        public override bool Attack() {
            if (!IsAbleToCast()) {
                return false;
            }
            
            CastSpell();
            return true;
        }

        private void CastSpell() {
            transform.LookAt(player);
            spell.Cast(transform);
            _nextAvailableCastTime = Time.time + spell.GetCooldown();
        }

        private bool IsAbleToCast() {
            return Time.time >= _nextAvailableCastTime;
        }
    }
}
