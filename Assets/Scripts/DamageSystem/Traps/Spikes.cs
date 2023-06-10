using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;

namespace DamageSystem.Traps {
    public class Spikes : MonoBehaviour {
        [SerializeField] private Animator animator;
        [Tooltip("Stops spikes from extending and retracting")]
        [SerializeField] private bool freeze = false;
        [Tooltip("Default position of spikes")]
        [SerializeField] private bool initiallyExtended = false;
        [Tooltip("Time before first extension/retraction. Useful for creating patterns with multiple spikes.")]
        [SerializeField] private float startDelay = 0f;
        [Tooltip("Time since start of extending to start of retracting")]
        [SerializeField] private float extendedTime = 3f;
        [Tooltip("Time since start of retracting to start of extending")]
        [SerializeField] private float retractedTime = 3f;
        [SerializeField] private List<AttackElemental> attackElementalsList;
        private DamageInfo damageInfo;
        private bool extended = false;
        private float lastExtendTime = 0f;
        private float lastRetractTime = 0f;

        void Start() {
            damageInfo = new DamageInfo(attackElementalsList, gameObject);
            if (initiallyExtended) {
                animator.Play("Extend instantly");
                extended = true;
            }
            lastExtendTime = Time.time + startDelay;
            lastRetractTime = Time.time + startDelay;
        }

        public void DealDamage(Damageable damageable) {
            damageable.TakeDamage(damageInfo);
        }

        void Update() {
            if (freeze) return;
            if (extended) {
                if (lastExtendTime + extendedTime < Time.time) {
                    Retract();
                }
            } else {
                if (lastRetractTime + retractedTime < Time.time) {
                    Extend();
                }
            }
        }

        void Extend() {
            animator.Play("Extend");
            lastExtendTime = Time.time;
            extended = true;
        }

        void Retract() {
            animator.Play("Retract");
            lastRetractTime = Time.time;
            extended = false;
        }
    }
}
