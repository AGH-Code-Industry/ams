using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;

using Enemies;

namespace Destroyable {
    public class Explosion : MonoBehaviour {
        [SerializeField] private float radius = 3f;
        [SerializeField] private float force = 10f;
        [SerializeField] private float upwardsForceModifier = 10f;
        [SerializeField] private float dealDamageTriggerEnabledTime = .1f;
        [SerializeField] private float getUpDelay = 1f;
        [SerializeField] private List<AttackElemental> attackElementalsList;
        private DamageInfo damageInfo;
        private SphereCollider damageTrigger;

        private void Awake() {
            damageInfo = new DamageInfo(attackElementalsList, gameObject);
            SetRadius();
        }

        private void OnEnable() {
            StartCoroutine(DisableDealDamageTriggerAfterDelay());
        }

        private void OnValidate() {
            SetRadius();
        }

        private void SetRadius() {
            damageTrigger = GetComponent<SphereCollider>();
            damageTrigger.radius = radius;
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.gameObject.TryGetComponent<Damageable>(out var damageable)) return;

            if(other.gameObject.CompareTag("Barrel")) { 
                StartCoroutine(DealDamageDelayed(damageable, damageInfo, Random.Range(0.03f, 0.1f)));
            } else {
                damageable.TakeDamage(damageInfo);
            }

            if (!other.gameObject.TryGetComponent<Rigidbody>(out var rigidbody)) return;

            if (other.gameObject.TryGetComponent<NavMeshAgent>(out var navMeshAgent)) {
                StartCoroutine(AddExplosionForceDisablingNavMeshAgent(navMeshAgent, rigidbody));
            } else {
                rigidbody.AddExplosionForce(force, transform.position, radius, upwardsForceModifier, ForceMode.Impulse);
            }
        }

        IEnumerator DealDamageDelayed(Damageable damageable, DamageInfo damageInfo, float time = 0) {
            yield return new WaitForSeconds(time);

            if(damageable is not null) {
                damageable.TakeDamage(damageInfo);
            }
        }

        IEnumerator AddExplosionForceDisablingNavMeshAgent(NavMeshAgent navMeshAgent, Rigidbody rigidbody) {
            navMeshAgent.enabled = false;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            if(rigidbody.gameObject.TryGetComponent(out Enemy enemy)) {
                enemy.enabled = false;
            }

            yield return new WaitForEndOfFrame();
            if (!rigidbody) yield break;
            rigidbody.AddExplosionForce(force, transform.position, radius, upwardsForceModifier, ForceMode.Impulse);
            while (!rigidbody.IsSleeping()) {
                yield return new WaitForEndOfFrame();
                if (!rigidbody) yield break;
            }
            yield return new WaitForSeconds(getUpDelay);
            if (!navMeshAgent) yield break;

            navMeshAgent.enabled = true;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            if(enemy is not null) {
                enemy.enabled = true;
            }
        }

        IEnumerator DisableDealDamageTriggerAfterDelay() {
            yield return new WaitForSeconds(dealDamageTriggerEnabledTime);
            damageTrigger.enabled = false;
        }
    }
}
