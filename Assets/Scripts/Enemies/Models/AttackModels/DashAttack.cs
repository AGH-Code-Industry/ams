using System;
using System.Collections.Generic;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
using UnityEngine;

namespace Enemies.Models.AttackModels {
    public class DashAttack : AttackModel{
        
        [SerializeField] [Range(0, 5)] [Tooltip("Time between attacks in seconds")]
        private float coolDown = 1;
        
        [SerializeField] [Range(0, 50)] [Tooltip("How fast your entity will jump on target")]
        private float dashForce = 1;
        
        [SerializeField] [Tooltip("Damage of collision")]
        private AttackElemental damageInfo;
        
        [SerializeField] [Range(0, 1)] [Tooltip("Minimal time between damage dealing")]
        private float damageDealingCoolDown = 1;
        
        private float _nextAvailableAttackTime;
        private float _castingDashTime;
        private float _nextAvailableDamageDealTime;
        private Rigidbody _rb;

        public override void StartModel() {
            _rb = gameObject.GetComponent<Rigidbody>();
        }

        public override bool Attack() {
            transform.LookAt(player);
            
            if (!IsAbleToAttack()) {
                return false;
            }

            DashOnTarget();
            return true;
        }

        private void DashOnTarget() {
            _rb.AddForce(dashForce * transform.forward, ForceMode.Impulse);
            _nextAvailableAttackTime = Time.time + coolDown;
        }

        private bool IsAbleToAttack() {
            if (Time.time >= _nextAvailableAttackTime) {
                return true;
            }

            return false;
        }

        private void OnCollisionEnter(Collision collision) {
            if (!(Time.time > _nextAvailableDamageDealTime)) return;
            
            var list = new List<AttackElemental> {damageInfo};
            collision.collider.GetComponent<IDamageable>()?.TakeDamage(new DamageInfo(list, gameObject));
            _nextAvailableDamageDealTime = Time.time + damageDealingCoolDown;
        }
    }
}