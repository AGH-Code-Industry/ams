using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DamageSystem.ReceiveDamage.Elementals;

namespace DamageSystem.Traps {
    public class SpikesCollisionTrigger : MonoBehaviour {
        [SerializeField] Spikes spikes;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.GetComponent<Damageable>()) {
                spikes.DealDamage(other.gameObject.GetComponent<Damageable>());
            }
        }
    }
}