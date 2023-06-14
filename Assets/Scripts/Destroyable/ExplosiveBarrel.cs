using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        [SerializeField] private float afterExplosionGetUpDelay = 1f;

        [Header("Entities explosion force")]
        [SerializeField] private float explosionForce = 10f;
        [SerializeField] private float explosionUpwardsForceModifier = 10f;

        [Header("Barrel fragments explosion force")]
        [SerializeField] private float fragmentsExplosionForce = 10f;
        [SerializeField] private float fragmentsExplosionUpwardsForceModifier = 1f;

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
            audioSource.Play();
            StartCoroutine(EnableDealDamageTrigger());
            StartCoroutine(DestroyAfterDelay());
            StartCoroutine(AddFractureFragmentsExplosionForce());
        }

        IEnumerator AddFractureFragmentsExplosionForce() {
            Transform parentTrans;
            while (!(parentTrans = transform.Find("ModelFragments"))) {
                yield return new WaitForEndOfFrame();
            } 
            Rigidbody[] fragments = parentTrans.gameObject.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody fragment in fragments) {
                fragment.AddExplosionForce(fragmentsExplosionForce, transform.position, explosionRadius, fragmentsExplosionUpwardsForceModifier, ForceMode.Impulse);
            }
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
            if (!other.gameObject.TryGetComponent<Damageable>(out var damageable)) return;

            damageable.TakeDamage(damageInfo);

            if (!other.gameObject.TryGetComponent<Rigidbody>(out var rigidbody)) return;

            if (other.gameObject.TryGetComponent<NavMeshAgent>(out var navMeshAgent)) {
                StartCoroutine(AddExplosionForceDisablingNavMeshAgent(navMeshAgent, rigidbody));
            } else {
                rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpwardsForceModifier, ForceMode.Impulse);
            }
        }

        IEnumerator AddExplosionForceDisablingNavMeshAgent(NavMeshAgent navMeshAgent, Rigidbody rigidbody) {
            navMeshAgent.enabled = false;
            yield return new WaitForEndOfFrame();
            rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpwardsForceModifier, ForceMode.Impulse);
            while (!rigidbody.IsSleeping()) {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(afterExplosionGetUpDelay);
            navMeshAgent.enabled = true;
        }
    }
}
