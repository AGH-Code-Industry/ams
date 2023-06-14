using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;

namespace Destroyable {
    public class ExplosiveBarrel : MonoBehaviour, IExplosive {
        [SerializeField] private ExplosiveBarrelCollisionTrigger collisionTrigger;
        [SerializeField] private GameObject explosion;
        [SerializeField] private float explosionRadius = 3f;
        [SerializeField] private List<AttackElemental> attackElementalsList;
        [SerializeField] private float dealDamageTriggerEnabledTime = 1f;
        [SerializeField] private float destroyAfterExplosionDelay = 5f;
        [SerializeField] private float explosionForce = 40f;
        [SerializeField] private float explosionUpwardsForceModifier = 10f;
        [SerializeField] private float afterExplosionGetUpDelay = 1f;

        private AudioSource audioSource;
        private DamageInfo damageInfo;
        private SphereCollider dealDamageTrigger;


        private void Awake() {
            audioSource = GetComponent<AudioSource>();
            SetRadius();
            damageInfo = new DamageInfo(attackElementalsList, gameObject);
        }

        private void OnValidate() {
            SetRadius();
        }

        private void SetRadius() {
            dealDamageTrigger = GetComponent<SphereCollider>();
            dealDamageTrigger.radius = explosionRadius;
        }

        public void Explode() {
            explosion.SetActive(true);
            collisionTrigger.gameObject.SetActive(false);
            audioSource.Play();
            StartCoroutine(EnableDealDamageTrigger());
            StartCoroutine(DestroyAfterDelay());
        }

        IEnumerator EnableDealDamageTrigger() {
            dealDamageTrigger.enabled = true;
            yield return new WaitForSeconds(dealDamageTriggerEnabledTime);
            dealDamageTrigger.enabled = false;
        }

        IEnumerator DestroyAfterDelay() {
            yield return new WaitForSeconds(destroyAfterExplosionDelay);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent<Damageable>(out var damageable)) {
                damageable.TakeDamage(damageInfo);
                if (other.gameObject.TryGetComponent<Rigidbody>(out var rigidbody)) {
                    if (other.gameObject.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out var navMeshAgent)) {
                        StartCoroutine(AddExplosionForceDisablingNavMeshAgent(navMeshAgent, rigidbody));
                    } else {
                        rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpwardsForceModifier, ForceMode.Impulse);
                    }
                }
            }
        }

        IEnumerator AddExplosionForceDisablingNavMeshAgent(UnityEngine.AI.NavMeshAgent navMeshAgent, Rigidbody rigidbody) {
            navMeshAgent.enabled = false;
            yield return new WaitForEndOfFrame();
            rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpwardsForceModifier, ForceMode.Impulse);
            while (true) {
                yield return new WaitForEndOfFrame();
                if(rigidbody.IsSleeping() == true) {
                    yield return new WaitForSeconds(afterExplosionGetUpDelay);
                    navMeshAgent.enabled = true;
                    break;
                }
            }
        }
    }
}
