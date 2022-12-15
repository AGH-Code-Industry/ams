using System;
using System.Collections;
using System.Collections.Generic;
using Testing.Damage;
using UnityEngine;
using UnityEngine.Events;

namespace Testing.Damage {
    public class Damageable : MonoBehaviour, IDamageable {
        [SerializeField] private EntityInfo entity;

        public UnityEvent Died;
        
        private int _currentHealth;
        private List<Effect> effects;

        private void Start(){
            _currentHealth = entity.maxHealth;
        }

        public bool TakeDamage(DamageInfo damageInfo){
            int damageTaken = 0;
            if (entity.isInvincible) return false;
            
            foreach (var damageElemental in damageInfo.elementals) {
                damageTaken += damageElemental.elementalDamage;
            }

            _currentHealth = Math.Clamp(_currentHealth - damageTaken, entity.isImmortal ? 1 : 0, entity.maxHealth);

            if (_currentHealth == 0) { 
                Died.Invoke();
            }

            return true;
        }
    }
}
